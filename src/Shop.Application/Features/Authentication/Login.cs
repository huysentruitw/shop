namespace Shop.Application.Features.Authentication;

[ExtendObjectType<Mutation>]
public sealed class LoginMutation
{
    public async Task<MutationResult> Login(
        [Service] IAuthenticationService authenticationService,
        DataContext dataContext,
        LoginInput input)
    {
        var userInfo = await GetUserInfoByEmail(input.Email);
        if (!userInfo.HasValue)
            return MutationResult.WithoutSubject();

        var passwordVerificationResult = authenticationService.VerifyHashedPassword(userInfo.Value.PasswordHash, input.Password);

        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return MutationResult.WithoutSubject();

        if (passwordVerificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            await UpdateUserPasswordHash(userInfo.Value.Id, input.Password);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userInfo.Value.Id.ToString("N")),
        };

        await authenticationService.SignIn(claims);

        return MutationResult.WithoutSubject();

        async Task<(Guid Id, string PasswordHash)?> GetUserInfoByEmail(string email)
        {
            var info = await dataContext.Set<Organization.Entities.User>()
                .Where(x => x.Email == email)
                .Select(x => new { x.Id, x.PasswordHash })
                .SingleOrDefaultAsync();

            return info is null ? null : (Id: info.Id, PasswordHash: info.PasswordHash);
        }

        async Task UpdateUserPasswordHash(Guid id, string password)
        {
            var user = await dataContext.Set<Organization.Entities.User>()
                .Where(x => x.Id == id)
                .SingleAsync();
            user.PasswordHash = authenticationService.HashPassword(password);
            dataContext.Attach(user);
            dataContext.Entry(user).Property(x => x.PasswordHash).IsModified = true;
            await dataContext.SaveChangesAsync();
        }
    }
}

public sealed record LoginInput
{
    public required string Email { get; init; }

    public required string Password { get; init; }
}

internal sealed class LoginInputValidator : AbstractValidator<LoginInput>
{
    public LoginInputValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
