using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyOnline.Models.Candidate;
using PharmacyOnline.Services.Candidate;
using System.Security.Claims;

namespace PharmacyOnline.Controllers.Candidates
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICandidateRepo _candidateRepo;

        public AuthenticationController(ICandidateRepo candidateRepo)
        {
            _candidateRepo = candidateRepo;
        }

        [HttpPost]
        [Route("admin/register")]
        public async Task<IActionResult> registerAdmin(AdminModel model)
        {

            return Ok(await _candidateRepo.createAdminAccount(model));
        }

        [HttpPost]
        [Route("candidate/register")]
        public async Task<IActionResult> register(candidateModel model)
        {
           
            return Ok(await _candidateRepo.register(model));
        }

        [HttpPost]
        [Route("candidate/verify&resent/otp")]
        public async Task<IActionResult> verify(otpModel model)
        {
            return Ok(await _candidateRepo.verifyOtp(model));

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(loginModel model)
        {
            return Ok( await _candidateRepo.login(model) );
        }

       

        /* [HttpPost]
        [Route("resend/otp")]
        public async Task<IActionResult> resendOtp(sentAgainOTP model)
        {
            return Ok( await _candidateRepo.sendAganOtp(model) );
        }*/

        [HttpPost]
        [Route("candidate/refreshtoken")]
        public async Task<IActionResult> refreshToken(refreshTokenModel model)
        {
            return Ok( await  _candidateRepo.refreshToken(model) );
        }

        [HttpPost, Authorize(Roles = "Candidate")]
        [Route("candidate/logout")]
        public async Task<IActionResult> logout(logoutModel model)
        {

            #region CHECK AUTHORIZATION & isUse
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if ( !identity.IsAuthenticated )
            {
                return Unauthorized("invalid  token! please try later");
            }

            int IdCategory = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (model.id != IdCategory)
            {
                return Forbid("you do not have permission to access the data ");
            }

            /* CHECK HACKER GET REFRESHTOKEN
                 var candidate = _context.Candidate.FirsOrDefault( c => c.Id == IdCategory );
                 if(candidate == null ) return NotFound("your account is not exist");
                // check this account has been used at the same time
                 if( candidate.isUse == true ) return Forbid("Error! has error occurred... please login again");
                    
             */

            #endregion

            return Ok( await _candidateRepo.logout(model) );
        }

        [HttpPost]
        [Route("candidate/login/google")]
        public async Task<IActionResult> authenByGoogle(GoogleTokenRequest model)
        {
            return Ok( await _candidateRepo.loginGoogle(model) );
        }


    }
}
