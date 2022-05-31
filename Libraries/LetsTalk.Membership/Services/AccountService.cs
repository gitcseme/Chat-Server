using LetsTalk.Shared.DTO;
using LetsTalk.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LetsTalk.Membership.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, 
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserResponseDto> RegisterAsync(RegisterModel model)
    {
        var user = new ApplicationUser
        {
            NickName = model.NickName,
            Email = model.Email,
            UserName = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return PrepareUserResponse(user);
        }

        var error = result.Errors.FirstOrDefault()?.Description;
        throw new Exception(error);
    }

    public async Task<UserResponseDto> LoginAsync(LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
            return PrepareUserResponse(await _userManager.FindByEmailAsync(model.Email));
        
        throw new Exception("Email or password is invalid");
    }

    public async Task<UserResponseDto> GetUserInfoAsync() =>
        PrepareUserResponse(await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User));

    public async Task LogoutAsync() =>
        await _signInManager.SignOutAsync();

    public async Task<IEnumerable<UserResponseDto>> GetUsersAsync()
    {
        var loggedInUser = await GetUserInfoAsync();
        var usersResponse = await _userManager.Users
            .Where(u => u.Id != loggedInUser.Id)
            .Select(u => PrepareUserResponse(u))
            .ToListAsync();

        return usersResponse;
    }

    private static UserResponseDto PrepareUserResponse(ApplicationUser user) =>
        new UserResponseDto(user.Id, user.NickName, user.Email);
    
}