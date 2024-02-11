using Microsoft.AspNetCore.Identity;

namespace Shop.Infrastructure.Authentication;

internal sealed class PasswordHasher : IPasswordHasher
{
    private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);
    }
}

public interface IPasswordHasher
{
    string HashPassword(string password);

    PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
}

internal sealed record User { }
