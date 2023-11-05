using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.Sevices.Dtos.UserDto;
using Talabat.Sevices.IServices.ITokenServices;
using Talabat.Sevices.IServices.IUserServices;

namespace Talabat.Sevices.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;

        public UserServices(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenServices tokenServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user != null)
                return null;

            var appUser = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split('@')[0]
            };

            var result = await _userManager.CreateAsync(appUser , registerDto.Password);

            if (!result.Succeeded)
                return null;

            return new UserDto
            {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Token = _tokenServices.CreateToken(appUser)
            };
        }

        public async Task<UserDto> LogInAsync(LogInDto logInDto)
        {
            var user = await _userManager.FindByEmailAsync(logInDto.Email);

            if (user is null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, logInDto.Password,false);

            if (!result.Succeeded)
                return null;

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenServices.CreateToken(user)
            };

        }

    }
}
