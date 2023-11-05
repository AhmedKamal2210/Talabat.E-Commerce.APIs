using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.E_Commerce1.HandleRespones;
using Talabat.Sevices.Dtos.UserDto;
using Talabat.Sevices.IServices.ITokenServices;
using Talabat.Sevices.IServices.IUserServices;

namespace Talabat.E_Commerce1.Controllers
{
    public class AccountController : BaseApiController
    { 
        private readonly IUserServices _userServices;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserServices userServices , UserManager<AppUser> userManager)
        {
            _userServices = userServices;
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> UserRegister(RegisterDto registerDto)
        {
            var user = await _userServices.RegisterAsync(registerDto);

            if (user == null)
                return BadRequest(new ApiException(400, "This Email is Already Exist"));

            return Ok(user);
        }

        [HttpPost("LogIn")]
        public async Task<ActionResult<UserDto>> UserLognIn(LogInDto logInDto)
        {
            var user = await _userServices.LogInAsync(logInDto);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            return Ok(user);
        }

        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x =>x.Type == ClaimTypes.Email)?.Value;
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email
            };
        }


    }
}
