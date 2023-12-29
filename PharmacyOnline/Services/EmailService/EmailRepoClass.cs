using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using PharmacyOnline.Entities;
using PharmacyOnline.Models.ProfileModel;

namespace PharmacyOnline.Services.EmailService
{
    public class EmailRepoClass : IEmailService
    {

        private readonly IConfiguration _configuration;
        private readonly PharmacyOnlineContext _context;

        public EmailRepoClass(IConfiguration configuration, PharmacyOnlineContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public void sendOtp(string to, string body)
        {
            var email = new MimeMessage();

            email.From.Add( MailboxAddress.Parse( _configuration.GetSection("EMAIL:EmailUsername").Value ) );

            email.To.Add( MailboxAddress.Parse(to));

            email.Subject = "OTP'S PHARMACY ONLINE";

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<h3> YOUR OTP CODE IS:</h3></br><h4>{body}</h4>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect( _configuration.GetSection("EMAIL:EmailHost").Value, 587, SecureSocketOptions.StartTls );

            smtp.Authenticate(_configuration.GetSection("EMAIL:EmailUsername").Value, _configuration.GetSection("EMAIL:EmailPassword").Value);

            smtp.Send( email );
            smtp.Disconnect(true);
        }

        public void sendData(string to, emailModel body)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EMAIL:EmailUsername").Value));

            email.To.Add(MailboxAddress.Parse(to));

            email.Subject = "Feedback email about your resume submitted to the website: \"PharmacyOnline.com\"";

            string timenow = (DateTime.Now).ToString();

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<h3>pharmaceutical company Notifications:</h3>\r\n  Datetime: {timenow}\r\n" +
                $"</br>\r\n<h4>Thank you for your interest in our company, we are happy to receive your profile. We congratulate" +
                $" your profile for being of interest to our company's management, We would like to have an interview appointment with you to exchange more information</h4>\r\n<p>Interview address:" +
                $" {body.interviewAddress} </h>\r\n<p>Interview time: {body.appointment}</p>\r\n<p>Note: {body.content}" +
                $" </h>\r\n<br/>\r\n<p></br>Thank you for your time. \r\n</br>Best regards.</p>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EMAIL:EmailHost").Value, 587, SecureSocketOptions.StartTls);

            smtp.Authenticate(_configuration.GetSection("EMAIL:EmailUsername").Value, _configuration.GetSection("EMAIL:EmailPassword").Value);

            smtp.Send(email);
            smtp.Disconnect(true);
        }


    }
}
