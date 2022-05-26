using Microsoft.AspNetCore.Identity;

namespace LetsTalk.Shared.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string NickName { get; set; } = "No name";
}
