namespace PharmacyOnline.Services.EmailService
{
    public interface IEmailService
    {
        void sendOtp(string to, string body);
    }
}
