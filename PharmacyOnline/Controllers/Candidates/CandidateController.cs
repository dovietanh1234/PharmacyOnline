using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyOnline.Models.Candidate;
using PharmacyOnline.Services.Candidate;

namespace PharmacyOnline.Controllers.Candidates
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepo _candidateRepo;

        public CandidateController(ICandidateRepo candidateRepo)
        {
            _candidateRepo = candidateRepo;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register(candidateModel model)
        {
           
            return Ok(await _candidateRepo.register(model));
        }

        [HttpPost]
        [Route("verfyOTP")]
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

        [HttpPost]
        [Route("resend/otp")]
        public async Task<IActionResult> resendOtp(sentAgainOTP model)
        {
            return Ok( await _candidateRepo.sendAganOtp(model) );
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<IActionResult> refreshToken(refreshTokenModel model)
        {
            return Ok( await  _candidateRepo.refreshToken(model) );
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> logout(logoutModel model)
        {
            return Ok( await _candidateRepo.logout(model) );
        }

        [HttpPost]
        [Route("authen/google")]
        public async Task<IActionResult> authenByGoogle(GoogleTokenRequest model)
        {
            return Ok( await _candidateRepo.loginGoogle(model) );
        }



    }
}
