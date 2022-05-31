using LetsTalk.Shared.DTO;
using LetsTalk.Shared.Entities;

namespace LetsTalk.Membership.Services;

public interface IAccountService
{
    Task<UserResponseDto> RegisterAsync(RegisterModel model);
    Task<UserResponseDto> LoginAsync(LoginModel model);
    Task<UserResponseDto> GetUserInfoAsync();
    Task LogoutAsync();
    Task<IEnumerable<UserResponseDto>> GetUsersAsync();
}