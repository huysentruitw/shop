using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Identity = Microsoft.AspNetCore.Identity;

namespace Shop.Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    private static readonly Identity.IPasswordHasher<User> PasswordHasher = new Identity.PasswordHasher<User>();
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string HashPassword(string password)
        => PasswordHasher.HashPassword(null!, password);

    public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        => PasswordHasher.VerifyHashedPassword(null!, hashedPassword, providedPassword) switch
        {
            Identity.PasswordVerificationResult.Failed => PasswordVerificationResult.Failed,
            Identity.PasswordVerificationResult.Success => PasswordVerificationResult.Success,
            Identity.PasswordVerificationResult.SuccessRehashNeeded => PasswordVerificationResult.SuccessRehashNeeded,
            _ => PasswordVerificationResult.Failed,
        };

    public async Task SignIn(Claim[] claims, bool isPersistent = true)
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("No HTTP context available");

        var identity = new ClaimsIdentity(claims, "ApplicationCookie");
        var principal = new ClaimsPrincipal(identity);
        await httpContext.SignInAsync(principal, new AuthenticationProperties { IsPersistent = isPersistent });
    }

    public async Task SignOut()
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("No HTTP context available");

        await httpContext.SignOutAsync();
    }
}

public interface IAuthenticationService
{
    string HashPassword(string password);

    PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);

    Task SignIn(Claim[] claims, bool isPersistent = true);

    Task SignOut();
}

public enum PasswordVerificationResult
{
    Failed,
    Success,
    SuccessRehashNeeded,
}

internal sealed record User { }
