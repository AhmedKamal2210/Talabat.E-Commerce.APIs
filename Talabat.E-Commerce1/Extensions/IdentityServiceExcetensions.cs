using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.Repository;

namespace Talabat.E_Commerce1.Extensions
{
    public static class IdentityServiceExcetensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration _configuration)
        {
            var builder = services.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<IdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["Token:Issuer"],
                        ValidateAudience = false
                    };
                });

            return services; 
        }
    }
}
