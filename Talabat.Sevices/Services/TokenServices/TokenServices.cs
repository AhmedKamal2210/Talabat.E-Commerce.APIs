using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.Sevices.IServices.ITokenServices;

namespace Talabat.Sevices.Services.TokenServices
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _SecurityKey;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }
        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>  // JWT Token Body (PayLoad : Data)
            {
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.GivenName, appUser.DisplayName)
            };

            var creds = new SigningCredentials(_SecurityKey, SecurityAlgorithms.HmacSha256); 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Token:Issuer"],
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
