using Talabat.Sevices.Dtos.UserDto;

namespace Talabat.Sevices.IServices.IUserServices
{
    public interface IUserServices
    {
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<UserDto> LogInAsync(LogInDto logInDto);
        //Task<UserDto> GetCurrentUser();
    }
}
