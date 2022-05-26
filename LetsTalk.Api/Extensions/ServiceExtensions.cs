using LetsTalk.Membership;
using LetsTalk.Shared.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LetsTalk.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .WithOrigins("http://localhost:8080")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
    }

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("SqlConnection");
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseSqlServer(connectionString, b =>
            {
                b.MigrationsAssembly(typeof(Program).Assembly.FullName);
            });
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureCookies(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(config =>
        {
            config.LoginPath = "/Home/Signin";
            config.Cookie.SameSite = SameSiteMode.None;
            config.Events = new CookieAuthenticationEvents()
            {
                OnRedirectToLogin = context =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                }
            };
        });
    }
}
