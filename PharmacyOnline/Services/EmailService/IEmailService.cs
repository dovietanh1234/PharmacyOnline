using PharmacyOnline.Models.ProfileModel;

namespace PharmacyOnline.Services.EmailService
{
    public interface IEmailService
    {
        void sendOtp(string to, string body);
        void sendData(string to, emailModel body);
    }
}
