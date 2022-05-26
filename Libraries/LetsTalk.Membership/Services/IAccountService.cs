using LetsTalk.Shared.DTO;
using LetsTalk.Shared.Entities;

namespace LetsTalk.Membership.Services;

public interface IAccountService
{
    Task<ApplicationUser> RegisterAsync(RegisterModel model);
    Task<ApplicationUser> LoginAsync(LoginModel model);
    Task<ApplicationUser> GetUserInfo();
}