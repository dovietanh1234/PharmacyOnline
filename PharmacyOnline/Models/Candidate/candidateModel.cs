using PharmacyOnline.Models.ProfileModel;
using System.ComponentModel.DataAnnotations;

namespace PharmacyOnline.Models.Candidate
{
    public class candidateModel
    {
        [Required(ErrorMessage = "please enter username")]
        [MinLength(3, ErrorMessage = "enter min 3 character ")]
        [MaxLength(255, ErrorMessage = "enter max 255 character")]
        public string username { get; set; }

        [Required, EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "please enter password")]
        [MinLength(8, ErrorMessage = "enter min 3 character ")]
        [MaxLength(30, ErrorMessage = "your password has exceeded 30 characters ")]
        public string password { get; set; }

        [Required, Compare("password")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }

    /*
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
     */
    public class AdminModel
    {
        [Required(ErrorMessage = "please enter username")]
        [MinLength(3, ErrorMessage = "enter min 3 character ")]
        [MaxLength(255, ErrorMessage = "enter max 255 character")]
        public string username { get; set; } = string.Empty;

        [Required(ErrorMessage = "please enter password")]
        [MinLength(6, ErrorMessage = "enter min 3 character ")]
        [MaxLength(30, ErrorMessage = "your password has exceeded 30 characters ")]
        public string password { get; set; } = string.Empty;

        [Required, Compare("password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }


    public class otpModel
    {
        public string? otp { get; set; } = null;

        [Required, EmailAddress]
        public string email { get; set; }
    }

    public class loginModel
    {
        [Required(ErrorMessage = "please enter otp"), EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "please enter password")]
        public string password { get; set; }
    }


    public class payloadToken
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string role { get; set; }
    }

    public class tokens
    {
        public string accessToken { get; set; } = string.Empty;
        public string refreshToken { get; set; } = string.Empty;
        public int status { get; set; }
        public string statusMessage { get; set; } = string.Empty;
    }

    public class result
    {
        public int status { get; set; }
        public string statusMessage { get; set; } = string.Empty;
    }

    public class sentAgainOTP
    {

        [Required, EmailAddress]
        public string email { get; set; }
    }


    public class refreshTokenModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string refreshToken { get; set; }
    }

    public class logoutModel
    {
        [Required]
        public int id { get; set; }
    }

    public class GoogleTokenRequest
    {
        public string token { get; set; }
    }

    public class approvedCV
    {
        public string idProfileDetail { get; set; } = "";
        public int isQualified { get; set; } = 2;

        public emailModel? body { get; set; } = null;
    }

}
