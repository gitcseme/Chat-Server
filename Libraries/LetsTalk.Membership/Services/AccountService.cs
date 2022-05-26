using LetsTalk.Shared.DTO;
using LetsTalk.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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

    public async Task<ApplicationUser> RegisterAsync(RegisterModel model)
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
            return user;
        }

        var error = result.Errors.FirstOrDefault()?.Description;
        throw new Exception(error);
    }

    public async Task<ApplicationUser> LoginAsync(LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
            return await _userManager.FindByEmailAsync(model.Email);
        
        throw new Exception("Email or password is invalid");
    }

    public async Task<ApplicationUser> GetUserInfo() =>
        await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
    
}