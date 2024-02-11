using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Infrastructure.Authentication;

public static class InfrastructureBuilderExtensions
{
    public static InfrastructureBuilder AddAuthentication(this InfrastructureBuilder builder)
    {
        builder.Services
            .AddHttpContextAccessor()
            .AddSingleton<IAuthenticationService, AuthenticationService>();

        builder.Services
            .AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "Shop.Auth";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
            });

        return builder;
    }
}
