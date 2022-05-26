using LetsTalk.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LetsTalk.Membership;

public class UserDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        :base(options)
    {

    }
}
