using Google.Apis.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Macs;
using PharmacyOnline.Common;
using PharmacyOnline.Entities;
using PharmacyOnline.Models.Candidate;
using PharmacyOnline.Services.EmailService;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace PharmacyOnline.Services.Candidate
{
    public class CandidateRepoClass : ICandidateRepo
    {

        private readonly PharmacyOnlineContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public CandidateRepoClass(PharmacyOnlineContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<result> register(candidateModel model)
        {
            try
            {
                var oldCandidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == model.email);
                if (oldCandidate != null)
                {
                    return new result
                    {
                        status = 400,
                        statusMessage = "email has existed"
                    };
                }

                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);

                var hashed = BCrypt.Net.BCrypt.HashPassword(model.password, salt);

                #region HANDLE OTP
                // create otp
                string otp = await Common.HandleLogic.create_otp();

                // send mail for client (not have email service! do it later)
                //TẮT TẠM ĐỂ TEST
                _emailService.sendOtp(model.email, otp);

                //hash otp and save in DB:
                var otpHash = BCrypt.Net.BCrypt.HashPassword(otp, salt);
                var newOtp = new Otp
                {
                    Email = model.email,
                    OtpHash = otpHash,
                    OtpSpamNumber = 1,
                    OtpSpam = DateTime.Now.AddDays(1),
                    LimitTimeToSendOtp = DateTime.Now.AddMinutes(1),
                };
                #endregion

                var candidate = new Entities.Candidate
                {
                    Username = model.username,
                    Role = "Candidate",
                    Thumbnail = "https://i.stack.imgur.com/l60Hf.png",
                    Email = model.email,
                    Password = hashed,
                    CreatedAt = DateTime.Now,
                    IsAtive = false
                };

                await _context.Otps.AddAsync(newOtp);
                await _context.Candidates.AddAsync(candidate);
                await _context.SaveChangesAsync();
                 return new result
                {
                    status = 202,
                    statusMessage = "please check otp in your email"
                }; ;
            }
            catch (Exception ex)
            {
                return new result
                {
                    status = 401,
                    statusMessage = "Error! has error occured"
                };
            }

        }

       public async Task<result> verifyOtp(otpModel model)
        {
            try
            {
                var newOtp = await _context.Otps.Where(o => o.Email == model.email).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                if (newOtp == null)
                {
                    return new result
                    {
                        status = 404,
                        statusMessage = "not found your otp"
                    };
                }

                // verify otp -> is true or not:
                bool verifyOtp = BCrypt.Net.BCrypt.Verify(model.otp, newOtp.OtpHash);
                if (!verifyOtp)
                {
                    return new result
                    {
                        status = 401,
                        statusMessage = "otp is not correct"
                    };
                }

                // check expire time:
                if ( newOtp.LimitTimeToSendOtp < DateTime.Now )
                {
                    return new result
                    {
                        status = 401,
                        statusMessage = "this otp has expired"
                    };
                }

                var user = await _context.Candidates.FirstOrDefaultAsync(u => u.Email == model.email);
                if ( user == null )
                {
                    return new result
                    {
                        status = 404,
                        statusMessage = "your account is not exist"
                    };
                }
                
                var otpList = _context.Otps.Where(o => o.Email.Equals(model.email));
                _context.Otps.RemoveRange(otpList);
                user.IsAtive = true;
                await _context.SaveChangesAsync();
                return new result
                {
                    status = 200,
                    statusMessage = "verify successfully! your account is enable"
                };


            }
            catch(Exception ex) {
                return new result
                {
                    status = 401,
                    statusMessage = "Error! has error occured"
                };
            }
        }

        public async Task<tokens> login(loginModel model)
        {
            try
            {
                var can = await _context.Candidates.FirstOrDefaultAsync( c => c.Email == model.email );

                if ( can == null )
                {
                    return new tokens()
                    {
                        status = 404,
                        statusMessage = "email is incorrect"
                    };
                }

                if( can.IsAtive == false  )
                {
                    return new tokens()
                    {
                        status = 403,
                        statusMessage = "your account is not enable please active your account through otp"
                    };
                }

                bool result = BCrypt.Net.BCrypt.Verify(model.password, can.Password);

                if ( !result )
                {
                    return new tokens()
                    {
                        status = 401,
                        statusMessage = "password is incorrect"
                    };
                }

                // create token & refresh token to save in DB:

                var payload = new payloadToken
                {
                    id = can.Id,
                    email = model.email,
                    username = can.Username,
                    role = can.Role
                };

                var handleTokens = new HandleLogic(_configuration);

                string refToken = await handleTokens.createRefreshToken(payload);

                // SAVE OR ALTER REFRESH TOKEN IN DB FOLLOW idCandidate:

                var oldKeyToken = await _context.KeyTokens.FirstOrDefaultAsync(k => k.IdCandidate == can.Id);

                if ( oldKeyToken == null)
                {
                    KeyToken keyToken = new KeyToken()
                    {
                        IdCandidate = can.Id,
                        RefreshToken = refToken,
                        CreateAt = DateTime.Now.AddDays(1),
                    };

                    await _context.KeyTokens.AddAsync(keyToken);
                }
                else
                {
                    oldKeyToken.RefreshToken = refToken;
                    oldKeyToken.CreateAt = DateTime.Now.AddDays(1);
                    
                }
                

                // check in func use accessToken -> if isUse == true prevent accesstoken
                if ( can.IsUse == true )
                {
                    can.IsUse = false;
                }

                await _context.SaveChangesAsync();

                return new tokens()
                {
                    accessToken = await handleTokens.createToken(payload),
                    refreshToken = refToken,
                    status = 202,
                    statusMessage = "login successfully"
                };


            }
            catch(Exception ex)
            {
                return new tokens()
                {
                    status = 401,
                    statusMessage = $"Error! has error occured{ex}"
                };
            }
        }


        public async Task<result> sendAganOtp(sentAgainOTP model)
        {

            try
            {

                int limit = 5;
                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);

                // get newest OTP in DB -> to check limit in a day:
                var oldOtp = await _context.Otps.Where(o => o.Email == model.email).OrderByDescending(o => o.Id).FirstOrDefaultAsync();

                var user1 = await _context.Candidates.FirstOrDefaultAsync( c => c.Email == model.email );

                if ( oldOtp == null || user1 == null )
                {
                    return new result
                    {
                        status = 404,
                        statusMessage = "you have not createted an account yet"
                    };
                }

                if (user1.IsAtive == true)
                {
                    return new result
                    {
                        status = 401,
                        statusMessage = "your account has been activated"
                    };
                }

                if ( oldOtp.OtpSpamNumber > limit )
                {
                    // if throgh a day we will accept to continue sent 5 otps for client
                    if ( oldOtp.OtpSpam < DateTime.Now )
                    {
                        oldOtp.OtpSpam = DateTime.Now.AddDays(1);
                        oldOtp.OtpSpamNumber = 1;
                        // create new otp and sent for client:
                        string otp = await Common.HandleLogic.create_otp();
                        _emailService.sendOtp(model.email, otp);

                        var otpHash = BCrypt.Net.BCrypt.HashPassword(otp, salt);

                        oldOtp.OtpHash = otpHash;
                        oldOtp.LimitTimeToSendOtp = DateTime.Now.AddMinutes(1);

                        await _context.SaveChangesAsync();
                        return new result
                        {
                            status = 200,
                            statusMessage = "otp has been sent in your email"
                        };
                    }
                    else
                    {
                        // if less than a day and you've sent it more than 5 times:
                        return new result
                        {
                            status = 401,
                            statusMessage = "your number of otp in a day has expired"
                        };
                    }
                }

                // check to see if it's been more than 1 minutes
                if ( oldOtp.LimitTimeToSendOtp > DateTime.Now )
                {
                    return new result
                    {
                        status = 401,
                        statusMessage = "otp can be sent every 1 minute"
                    };
                }

                // accept for sent otp: 
                string validotp = await Common.HandleLogic.create_otp();
                _emailService.sendOtp(model.email, validotp);

                var validotpHash = BCrypt.Net.BCrypt.HashPassword(validotp, salt);

                oldOtp.OtpHash = validotpHash;
                oldOtp.OtpSpamNumber = oldOtp.OtpSpamNumber + 1;
                oldOtp.LimitTimeToSendOtp = DateTime.Now.AddMinutes(1);

                await _context.SaveChangesAsync();
                return new result
                {
                    status = 200,
                    statusMessage = "otp has been sent in your email"
                };


            }
            catch(Exception ex)
            {
                return new result
                {
                    status = 401,
                    statusMessage = $"Error! has error occured {ex}"
                };
            }

        }



        public async Task<tokens> refreshToken(refreshTokenModel model)
        {
            try
            {
                var oldKeyToken = await _context.KeyTokens.FirstOrDefaultAsync( k => k.IdCandidate == model.id );


                if( oldKeyToken == null)
                {
                    return new tokens()
                    {
                        status = 401,
                        statusMessage = "your refresh token has expired please try again!"
                    };
                }

                var can = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == oldKeyToken.IdCandidate);

                if (can == null)
                {
                    return new tokens()
                    {
                        status = 404,
                        statusMessage = "candidate has not exist!"
                    };
                }

                // handle if has valid refreshToken:
                if ( model.refreshToken == oldKeyToken.RefreshToken )
                {
                    if (oldKeyToken.CreateAt > DateTime.Now)
                    {
                        // save this token into refreshToken has used:
                        var tkUsed = new RefreshTokenUsed()
                        {
                            IdKeyToken = oldKeyToken.Id,
                            RefreshTokenUsed1 = oldKeyToken.RefreshToken
                        };

                        await _context.RefreshTokenUseds.AddAsync(tkUsed);

                        // return pair tokens and add new refresh token in KeyToken table:

                        var payload = new payloadToken
                        {
                            id = can.Id,
                            email = can.Email,
                            username = can.Username,
                            role = can.Role
                        };

                        var handleTokens = new HandleLogic(_configuration);

                        string refToken = await handleTokens.createRefreshToken(payload);

                        oldKeyToken.RefreshToken = refToken;
                        oldKeyToken.CreateAt = DateTime.Now.AddDays(1);

                        await _context.SaveChangesAsync();


                        return new tokens()
                        {
                            accessToken = await handleTokens.createToken(payload),
                            refreshToken = refToken,
                            status = 202,
                            statusMessage = "verify refreshToken success"
                        };
                    }
                    else
                    {
                        return new tokens()
                        {
                            status = 401,
                            statusMessage = $"Error! refresh token has expired"
                        };
                    }
                
                }

                // check refreshTokenUsed:
                var refreshTk = await _context.RefreshTokenUseds.FirstOrDefaultAsync(tk => tk.RefreshTokenUsed1 == model.refreshToken);

                if (refreshTk != null)
                {
                    // has value delete all things about tokens -> change isUse = true:
                    var listRefreshTk = await _context.RefreshTokenUseds.Where(rf => rf.IdKeyToken == oldKeyToken.Id).ToListAsync();

                    _context.RefreshTokenUseds.RemoveRange(listRefreshTk);
                    _context.KeyTokens.Remove(oldKeyToken);
                    can.IsUse = true;
                    await _context.SaveChangesAsync();
                    return new tokens()
                    {
                        status = 401,
                        statusMessage = $"refresh token has used please login again to get new token"
                    };
                }
                else
                {
                    return new tokens()
                    {
                        status = 401,
                        statusMessage = $"refresh token has expired please login again to get tokens"
                    };
                }

                
            }catch(Exception ex) { 
                return new tokens()
                {
                    status = 401,
                    statusMessage = $"Error! has error occured{ex}"
                };
            }
        }


        public async Task<result> logout(logoutModel model)
        {
            try
            {
                // HANDLE TOKEN & isUse



                var oldKeyToken = await _context.KeyTokens.FirstOrDefaultAsync(k => k.IdCandidate == model.id);
                if (oldKeyToken == null)
                {
                    return new result
                    {
                        status = 401,
                        statusMessage = "Error! you are not login"
                    };
                }
                var listRefreshTk = await _context.RefreshTokenUseds.Where(rf => rf.IdKeyToken == oldKeyToken.Id).ToListAsync();

                _context.RefreshTokenUseds.RemoveRange(listRefreshTk);
                _context.KeyTokens.Remove(oldKeyToken);
                await _context.SaveChangesAsync();

                return new result
                {
                    status = 200,
                    statusMessage = "Logout successfully"
                };

            }
            catch(Exception ex)
            {
                return new result
                {
                    status = 401,
                    statusMessage = "Error! has error occured"
                };
            }
        }

        public async Task<tokens> loginGoogle(GoogleTokenRequest model)
        {
            try
            {
                var payloadGG = await GoogleJsonWebSignature.ValidateAsync(model.token);

                if (payloadGG == null)
                {
                    return new tokens()
                    {
                        status = 401,
                        statusMessage = $"Login authen by google fail"
                    };
                }

                var can = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == payloadGG.Email);

                // if already has value -> create refresh token | if it has not value -> create new value!

                if ( can == null  )
                {
                    // create new Candidate -> create pair tokens
                    var candidate = new Entities.Candidate
                    {
                        Username = payloadGG.Name,
                        Role = "Candidate",
                        Thumbnail = payloadGG.Picture,
                        Email = payloadGG.Email,
                        CreatedAt = DateTime.Now,
                        IsAtive = true
                    };

                    await _context.Candidates.AddAsync(candidate);
                    await _context.SaveChangesAsync();

                    var payload = new payloadToken
                    {
                        id = candidate.Id,
                        email = candidate.Email,
                        username = candidate.Username,
                        role = candidate.Role
                    };

                    var handleTokens = new HandleLogic(_configuration);

                    string refToken = await handleTokens.createRefreshToken(payload);

                    // SAVE OR ALTER REFRESH TOKEN IN DB FOLLOW idCandidate:

                    var oldKeyToken = await _context.KeyTokens.FirstOrDefaultAsync(k => k.IdCandidate == candidate.Id);

                    if (oldKeyToken == null)
                    {
                        KeyToken keyToken = new KeyToken()
                        {
                            IdCandidate = candidate.Id,
                            RefreshToken = refToken,
                            CreateAt = DateTime.Now.AddDays(1),
                        };

                        await _context.KeyTokens.AddAsync(keyToken);
                    }
                    else
                    {
                        oldKeyToken.RefreshToken = refToken;
                        oldKeyToken.CreateAt = DateTime.Now.AddDays(1);

                    }

                    await _context.SaveChangesAsync();

                    return new tokens()
                    {
                        accessToken = await handleTokens.createToken(payload),
                        refreshToken = refToken,
                        status = 202,
                        statusMessage = "login successfully"
                    };

                }
                else
                {
                    // create pair tokens:
                    var payload = new payloadToken
                    {
                        id = can.Id,
                        email = can.Email,
                        username = can.Username,
                        role = can.Role
                    };

                    var handleTokens = new HandleLogic(_configuration);

                    string refToken = await handleTokens.createRefreshToken(payload);

                    // SAVE OR ALTER REFRESH TOKEN IN DB FOLLOW idCandidate:

                    var oldKeyToken = await _context.KeyTokens.FirstOrDefaultAsync(k => k.IdCandidate == can.Id);

                    if (oldKeyToken == null)
                    {
                        KeyToken keyToken = new KeyToken()
                        {
                            IdCandidate = can.Id,
                            RefreshToken = refToken,
                            CreateAt = DateTime.Now.AddDays(1),
                        };

                        await _context.KeyTokens.AddAsync(keyToken);
                    }
                    else
                    {
                        oldKeyToken.RefreshToken = refToken;
                        oldKeyToken.CreateAt = DateTime.Now.AddDays(1);

                    }


                    // check in func use accessToken -> if isUse == true prevent accesstoken
                    if (can.IsUse == true)
                    {
                        can.IsUse = false;
                    }

                    await _context.SaveChangesAsync();

                    return new tokens()
                    {
                        accessToken = await handleTokens.createToken(payload),
                        refreshToken = refToken,
                        status = 202,
                        statusMessage = "login successfully"
                    };



                }

            }
            catch (Exception ex)
            {
                return new tokens()
                {
                    status = 401,
                    statusMessage = $"Error! has error occured{ex}"
                };
            }
        }







    }
}
