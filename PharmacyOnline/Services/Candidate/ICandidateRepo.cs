using PharmacyOnline.Models.Candidate;

namespace PharmacyOnline.Services.Candidate
{
    public interface ICandidateRepo
    {
        Task<result> register(candidateModel model);

        Task<result> verifyOtp(otpModel model);

        Task<tokens> login(loginModel model);

        Task<result> sendAganOtp(sentAgainOTP model);

        Task<tokens> refreshToken(refreshTokenModel model);

        Task<result> logout(logoutModel model);

        Task<tokens> loginGoogle(GoogleTokenRequest model);
    }
}
