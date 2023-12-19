using Microsoft.IdentityModel.Tokens;
using PharmacyOnline.Models.Candidate;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharmacyOnline.Common
{
    public class HandleLogic
    {

        private readonly IConfiguration _configuration;

        public HandleLogic(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       public static async Task<string> create_otp()
        {
            Random random = new Random();
            int number = random.Next(10000, 100000);

            // Task<string> randomString = Task.Run( () => number.ToString() );
            // string result = await randomString;
            // return result;
            return await Task.FromResult(number.ToString());
        }

        public async Task<string> createToken(payloadToken payload)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _configuration["JWT:Key"]  ));

            var signatureKey = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var payload2 = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, payload.id.ToString()),
                new Claim(ClaimTypes.Email, payload.email),
                new Claim(ClaimTypes.Name, payload.username),
                new Claim(ClaimTypes.Role, payload.role),
            };

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                payload2,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signatureKey
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> createRefreshToken(payloadToken payload)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var signatureKey = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var payload2 = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, payload.id.ToString()),
                new Claim(ClaimTypes.Email, payload.email),
                new Claim(ClaimTypes.Name, payload.username),
                new Claim(ClaimTypes.Role, payload.role),
            };

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                payload2,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signatureKey
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
