using LetsTalk.Membership.Services;
using LetsTalk.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetsTalk.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = await _accountService.RegisterAsync(model);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _accountService.LoginAsync(model);
            return Ok(user);
        }
        
        [HttpGet]
        [Route("user-info")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _accountService.GetUserInfo();
            return Ok(user);
        }
    }
}
