using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PharmacyOnline.Common;
using PharmacyOnline.DTO.ProfileDTO;
using PharmacyOnline.Entities;
using PharmacyOnline.Models.ProfileModel;
using PharmacyOnline.Services.EmailService;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PharmacyOnline.Services.ProfileService
{
    public class ProfileRepoClass : IProfileRepo
    {
        private readonly PharmacyOnlineContext _context;
        private readonly int _TIMESTOCREATE = 20;
        private readonly int _PAGESIZE = 10;
        private readonly IEmailService _IEmailService;

        public ProfileRepoClass(PharmacyOnlineContext context, IEmailService IEmailService)
        {
            _context = context;
            _IEmailService = IEmailService;
        }

        public async Task<object> createProfile(ProfileModel model, int idCandidate, string fileThumbnail, string fileCV)
        {
            try
            {
                var Can = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == idCandidate);
                if (Can == null)
                {
                    return new Models.Candidate.result
                    {
                        status = 404,
                        statusMessage = "not found data"
                    };
                }


                Can.QuantityProfile = Can.QuantityProfile + 1;


                PersonalDetail PsDt = new PersonalDetail()
                {
                    Id = await Common.HandleLogic.randomStr(),
                    CandidateId = idCandidate,
                    Fullname = model.Fullname,
                    Address = model.Address,
                    Number = model.Number,
                    Email = model.Email,
                    Thumbnail = fileThumbnail,
                    FileCv = fileCV,
                    Skills = model.Skills,
                    UniversityOrCollege = model.UniversityOrCollege,
                    Major = model.Major,
                    IssuedDate = model.IssuedDate,
                    ExpiryDate = model.ExpiryDate,
                    ScientificAchievements = model.ScientificAchievements,
                    WorkExperiences = model.WorkExperiences,
                    Reference = model.Reference,
                    Status = "EDIT",
                    CreatedAt = DateTime.Now,
                    IsAccepted = "PENDING",
                    Age = model.Age,
                    DateOfBirth = model.DateOfBirth,
                    Gender= model.Gender,
                };

                await _context.PersonalDetails.AddAsync(PsDt);
                await _context.SaveChangesAsync();

                return new
                {
                    Id = PsDt.Id,
                    CandidateId = idCandidate,
                    Fullname = PsDt.Fullname,
                    Age = PsDt.Age,
                    DateOfBirth = PsDt.DateOfBirth,
                    Gender = PsDt.Gender,
                    Address = PsDt.Address,
                    Number = PsDt.Number,
                    Email = PsDt.Email,
                    Thumbnail = fileThumbnail,
                    FileCv = fileCV,
                    Skills = PsDt.Skills,
                    UniversityOrCollege = PsDt.UniversityOrCollege,
                    Major = PsDt.Major,
                    IssuedDate = PsDt.IssuedDate,
                    ExpiryDate = PsDt.ExpiryDate,
                    ScientificAchievements = PsDt.ScientificAchievements,
                    WorkExperiences = PsDt.WorkExperiences,
                    Reference = PsDt.Reference,
                    CreatedAt = DateTime.Now,
                    IsAccepted = "PENDING"
                };
            }
            catch (Exception ex)
            {
                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = ex.Message
                };
            }
        }

        // cái này chưa lấy ra
        public async Task<object> detailProfile(string idProfileDetail)
        {
            try
            {
                var PsDt = await _context.PersonalDetails.FirstOrDefaultAsync(x => x.Id == idProfileDetail);

                if (PsDt == null)
                {
                    return new Models.Candidate.result
                    {
                        status = 404,
                        statusMessage = "not found the data"
                    };
                }

                return new
                {
                    Id = PsDt.Id,
                    Fullname = PsDt.Fullname,
                    Age = PsDt.Age,
                    DateOfBirth = PsDt.DateOfBirth,
                    Gender = PsDt.Gender,
                    Address = PsDt.Address,
                    Number = PsDt.Number,
                    Email = PsDt.Email,
                    Thumbnail = PsDt.Thumbnail,
                    FileCv = PsDt.FileCv,
                    Skills = PsDt.Skills,
                    UniversityOrCollege = PsDt.UniversityOrCollege,
                    Major = PsDt.Major,
                    IssuedDate = PsDt.IssuedDate,
                    ExpiryDate = PsDt.ExpiryDate,
                    ScientificAchievements = PsDt.ScientificAchievements,
                    WorkExperiences = PsDt.WorkExperiences,
                    Reference = PsDt.Reference,
                    status = PsDt.Status,
                    CreatedAt = PsDt.CreatedAt,
                    updateAt = PsDt.UpdatedAt,
                    IsAccepted = PsDt.IsAccepted
                };
            }
            catch (Exception ex)
            {
                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = ex.Message
                };
            }
        }

        public async Task<object> updateProfile(ProfileModel model, string idProfileDetail, string fileThumbnail, string fileCV)
        {
            try
            {

                var profileDT = await _context.PersonalDetails.FirstOrDefaultAsync(pf => pf.Id == idProfileDetail);

                if (profileDT == null)
                {
                    return new Models.Candidate.result
                    {
                        status = 404,
                        statusMessage = "not found data"
                    };
                }

                /*                #region check times to create
                var numberCreateP = await _context.Candidates.FirstOrDefaultAsync(ps => ps.Id == profileDT.CandidateId);
                if ( numberCreateP != null )
                {
                    if(numberCreateP.QuantityProfile > _TIMESTOCREATE || profileDT.CreatedAt.AddDays(8) < DateTime.Now)
                    {
                        return new Models.Candidate.result
                        {
                            status = 404,
                            statusMessage = "times you create or edit limit 5 times in a week please try later"
                        };
                    }
                }
                #endregion*/

                if (profileDT.Status == "SUBMITTED")
                {
                    return new Models.Candidate.result
                    {
                        status = 404,
                        statusMessage = "form has sumbitted, it isn't editing"
                    };
                }

                profileDT.Fullname = model.Fullname == "" ? profileDT.Fullname : model.Fullname;
                profileDT.Address = model.Address == "" ? profileDT.Address : model.Address;
                profileDT.Number = model.Number == "" ? profileDT.Number : model.Number;
                profileDT.Email = model.Email == "" ? profileDT.Email : model.Email;
                profileDT.Thumbnail = fileThumbnail == "" ? profileDT.Thumbnail : fileThumbnail;
                profileDT.FileCv = fileCV == "" ? profileDT.FileCv : fileCV;
                profileDT.Skills = model.Skills == "" ? profileDT.Skills : model.Skills;
                profileDT.UniversityOrCollege = model.UniversityOrCollege == "" ? profileDT.UniversityOrCollege : model.UniversityOrCollege;
                profileDT.Major = model.Major == "" ? profileDT.Major : model.Major;
                profileDT.IssuedDate = model.IssuedDate == null ? profileDT.IssuedDate : model.IssuedDate;
                profileDT.ExpiryDate = model.ExpiryDate == null ? profileDT.ExpiryDate : model.ExpiryDate;
                profileDT.ScientificAchievements = model.ScientificAchievements == "" ? profileDT.ScientificAchievements : model.ScientificAchievements;
                profileDT.WorkExperiences = model.WorkExperiences == "" ? profileDT.WorkExperiences : model.WorkExperiences;
                profileDT.Reference = model.Reference == "" ? profileDT.Reference : model.Reference;
                profileDT.Status = "EDIT";
                profileDT.UpdatedAt = DateTime.Now;
                profileDT.IsAccepted = "PENDING";
                profileDT.Age = model.Age == ""? profileDT.Age:model.Age;
                profileDT.DateOfBirth = model.DateOfBirth == null ? profileDT.DateOfBirth : model.DateOfBirth;
                profileDT.Gender = model.Gender == ""?profileDT.Gender:model.Gender;


                await _context.SaveChangesAsync();

                return new
                {
                    Id = profileDT.Id,
                    CandidateId = profileDT.CandidateId,
                    Fullname = profileDT.Fullname,
                    Age = profileDT.Age,
                    DateOfBirth = profileDT.DateOfBirth,
                    Gender = profileDT.Gender,
                    Address = profileDT.Address,
                    Number = profileDT.Number,
                    Email = profileDT.Email,
                    Thumbnail = profileDT.Thumbnail,
                    FileCv = profileDT.FileCv,
                    Skills = profileDT.Skills,
                    UniversityOrCollege = profileDT.UniversityOrCollege,
                    Major = profileDT.Major,
                    IssuedDate = profileDT.IssuedDate,
                    ExpiryDate = profileDT.ExpiryDate,
                    ScientificAchievements = profileDT.ScientificAchievements,
                    WorkExperiences = profileDT.WorkExperiences,
                    Reference = profileDT.Reference,
                    CreatedAt = profileDT.CreatedAt,
                    UpdatedAt = DateTime.Now,
                    IsAccepted = "PENDING"
                };


            }
            catch (Exception ex)
            {
                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = $"bad request{ex.Message}"
                };
            }
        }


        public async Task<object> cancelSubmitted(string idProfileDetail)
        {
            try
            {
                var profileDT = await _context.PersonalDetails.FirstOrDefaultAsync(pf => pf.Id == idProfileDetail);

                if (profileDT == null)
                {
                    return new Models.Candidate.result
                    {
                        status = 404,
                        statusMessage = "not found data"
                    };
                }

                if (profileDT.Status == "SUBMITTED" && profileDT.IsAccepted == "PENDING")
                {
                    // cho phép huỷ và chỉnh sửa lại hồ sơ:
                    profileDT.Status = "EDIT";
                    profileDT.IsAccepted = "PENDING";
                    await _context.SaveChangesAsync();
                    return new Models.Candidate.result
                    {
                        status = 200,
                        statusMessage = "cancel submit successful, you can edit your profile now!"
                    };
                }

                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = "your profile has not submitted!"
                };


            }
            catch (Exception ex)
            {
                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = $"bad request{ex.Message}"
                };
            }
        }

        public async Task<object> deleteSubmitted(string idProfileDetail)
        {
            try
            {
                var profileDT = await _context.PersonalDetails.FirstOrDefaultAsync(pf => pf.Id == idProfileDetail);

                if (profileDT == null)
                {
                    return new Models.Candidate.result
                    {
                        status = 404,
                        statusMessage = "not found data"
                    };
                }
                if (profileDT.Status == "EDIT" && profileDT.IsAccepted == "PENDING")
                {

                    _context.PersonalDetails.Remove(profileDT);
                    await _context.SaveChangesAsync();
                    return new Models.Candidate.result
                    {
                        status = 204,
                        statusMessage = "delete profile sccesssful!"
                    };
                }

                // chỉ cho phép xoá những profile đang có trạng thái edit và isStatus là pending
                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = "your profile is not in the status to can delete! please try later!"
                };

            }
            catch (Exception ex)
            {
                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = $"bad request{ex.Message}"
                };
            }
        }

        public async Task<object> Submit(string idProfileDetail)
        {
            try
            {
                var profileDT = await _context.PersonalDetails.FirstOrDefaultAsync(pf => pf.Id == idProfileDetail);

                if (profileDT == null)
                {
                    return new Models.Candidate.result
                    {
                        status = 404,
                        statusMessage = "not found data"
                    };
                }
                if (profileDT.Status == "EDIT" && profileDT.IsAccepted == "PENDING")
                {

                    profileDT.Status = "SUBMITTED";

                    await _context.SaveChangesAsync();
                    return new Models.Candidate.result
                    {
                        status = 200,
                        statusMessage = "Submit resume successfully!"
                    };
                }

                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = "Cannot Submit this profile because your Profile is not in Status can submit! please edit or create new to submit"
                };

            }
            catch (Exception ex)
            {
                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = $"bad request{ex.Message}"
                };
            }
        }


        public async Task<List<ProfileDTO>> GetPLocal(int candidateId)
        {
            List<ProfileDTO> newL = new List<ProfileDTO>();
            try
            {
                var listP = _context.PersonalDetails.AsQueryable().Where(p => (p.Status == "EDIT" && p.CandidateId == candidateId || p.Status == "SUBMITTED" && p.CandidateId == candidateId)).OrderByDescending(p => p.CreatedAt);

                var personalDT = PaginationList<Entities.PersonalDetail>.Create(listP, 1, _PAGESIZE);

                if (personalDT.Count == 0)
                {
                    return newL;
                }

                foreach (var p in personalDT)
                {
                    newL.Add(new ProfileDTO
                    {
                        Id = p.Id,
                        fullname = p.Fullname,
                        Email = p.Email,
                        University = p.UniversityOrCollege,
                        status = p.Status,
                        isAccepted = p.IsAccepted
                    });
                }

                return newL;

            }
            catch (Exception ex)
            {
                return newL;
            }
        }


        public async Task<List<ProfileDTO>> GetHistory(int candidateId)
        {
            List<ProfileDTO> newL = new List<ProfileDTO>();
            try
            {

                var listP = _context.PersonalDetails.AsQueryable().Where(p => (p.Status == "CHECKED" && p.CandidateId == candidateId)).OrderByDescending(p => p.CreatedAt);

                var personalDT = PaginationList<Entities.PersonalDetail>.Create(listP, 1, _PAGESIZE);

                if (personalDT.Count == 0)
                {
                    return newL;
                }

                foreach (var p in personalDT)
                {
                    newL.Add(new ProfileDTO
                    {
                        Id = p.Id,
                        fullname = p.Fullname,
                        Email = p.Email,
                        University = p.UniversityOrCollege,
                        status = p.Status,
                        isAccepted = p.IsAccepted
                    });
                }

                return newL;

            }
            catch (Exception ex)
            {
                return newL;
            }
        }


        //  ADMIN   UNQUALIFIED | QUALIFIED
        public async Task<List<ProfileDTO>> GetHistoryProfileAdmin(int page, string? isQualified)
        {
            List<ProfileDTO> newL = new List<ProfileDTO>();
            try
            {

                var listP = _context.PersonalDetails.AsQueryable();

                switch (isQualified)
                {

                    case "UNQUALIFIED":
                         listP = listP.Where(p => (p.Status == "CHECKED" && p.IsAccepted == "UNQUALIFIED")).OrderByDescending(p => p.CreatedAt);
                        break;
                    case "QUALIFIED":
                        listP = listP.Where(p => (p.Status == "CHECKED" && p.IsAccepted == "QUALIFIED")).OrderByDescending(p => p.CreatedAt);
                        break;
                    default: 
                        listP = listP.Where(p => p.Status == "CHECKED").OrderByDescending(p => p.CreatedAt);
                        break;
                }

                var personalDT = PaginationList<Entities.PersonalDetail>.Create(listP, page, _PAGESIZE);

                if (personalDT.Count == 0)
                {
                    return newL;
                }

                foreach (var p in personalDT)
                {
                    newL.Add(new ProfileDTO
                    {
                        Id = p.Id,
                        fullname = p.Fullname,
                        Email = p.Email,
                        University = p.UniversityOrCollege,
                        status = p.Status,
                        isAccepted = p.IsAccepted
                    });
                }

                return newL;

            }
            catch (Exception ex)
            {
                return newL;
            }
        }


        public async Task<List<ProfileDTO>> GetListSubmittedAdmin(int page)
        {
            List<ProfileDTO> newL = new List<ProfileDTO>();
            try
            {

                var listP = _context.PersonalDetails.AsQueryable().Where(p => (p.Status == "SUBMITTED")).OrderByDescending(p => p.CreatedAt);

                var personalDT = PaginationList<Entities.PersonalDetail>.Create(listP, page, _PAGESIZE);

                if (personalDT.Count == 0)
                {
                    return newL;
                }

                foreach (var p in personalDT)
                {
                    if (p.CreatedAt.AddDays(15) < DateTime.Now)
                    {
                        p.Status = "CHECKED";
                        p.IsAccepted = "UNQUALIFIED";

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        newL.Add(new ProfileDTO
                        {
                            Id = p.Id,
                            fullname = p.Fullname,
                            Email = p.Email,
                            University = p.UniversityOrCollege,
                            status = p.Status,
                            isAccepted = p.IsAccepted
                        });
                    }


                }

                return newL;

            }
            catch (Exception ex)
            {
                return newL;
            }
        }


        public async Task<List<ProfileDTO>> searchSdtGmailAdmin(string search, int page)
        {
            List<ProfileDTO> newL = new List<ProfileDTO>();
            try
            {

                var listP = _context.PersonalDetails.AsQueryable().Where(p => (p.Number.Contains(search) || p.Email.Contains(search) ));

                var personalDT = PaginationList<Entities.PersonalDetail>.Create(listP, page, _PAGESIZE);

                if (personalDT.Count == 0)
                {
                    return newL;
                }

                foreach (var p in personalDT)
                {
                    if (p.Status != "EDIT")
                    {
                        newL.Add(new ProfileDTO
                        {
                            Id = p.Id,
                            fullname = p.Fullname,
                            Email = p.Email,
                            Number = p.Number,
                            University = p.UniversityOrCollege,
                            status = p.Status,
                            isAccepted = p.IsAccepted
                        });
                    }
                }

                return newL;

            }
            catch (Exception ex)
            {
                return newL;
            }
        }





        public async Task<object> ApprovingAdmin(string idProfileDetail, int isQualified, emailModel? body)
        {
            try
            {
                var Element = await _context.PersonalDetails.FirstOrDefaultAsync(p => p.Id == idProfileDetail);
                if (Element == null) return new Models.Candidate.result
                {
                    status = 404,
                    statusMessage = "not found the CV"
                };
                if ( Element.Status != "SUBMITTED")
                {
                    return new Models.Candidate.result
                    {
                        status = 401,
                        statusMessage = $"Client doesnt submit this profile"
                    };
                }

                var candidate = _context.Candidates.FirstOrDefault(c => c.Id == Element.CandidateId);

                // isQualified = 1 là được duyệt, status = 2 bị loại 

                switch (isQualified)
                {
                    case 1:
                        Element.Status = "CHECKED";
                        Element.IsAccepted = "QUALIFIED";

                        string emailToSent = body.to == "" ? candidate.Email : body.to;

                        if (!string.IsNullOrEmpty(emailToSent) && body != null)
                        {
                            _IEmailService.sendData(emailToSent, body);
                        }

                        await _context.SaveChangesAsync();
                        return new Models.Candidate.result
                        {
                            status = 200,
                            statusMessage = $"resume id: {idProfileDetail} is Qualified!"
                        };
                    case 2:
                        Element.Status = "CHECKED";
                        Element.IsAccepted = "UNQUALIFIED";

                        await _context.SaveChangesAsync();
                        return new Models.Candidate.result
                        {
                            status = 201,
                            statusMessage = $"resume id: {idProfileDetail} is Unqualified!"
                        };

                    default:
                        return new Models.Candidate.result
                        {
                            status = 401,
                            statusMessage = "status has not accepted"
                        };

                }

            }
            catch (Exception ex)
            {
                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = ex.Message
                };
            }

        }


    }
}

/*
             try
            {

            }catch(Exception ex)
            {
                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = $"bad request{ex.Message}"
                };
            }
 */