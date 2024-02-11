namespace Shop.Application.Features.Authentication;

[ExtendObjectType<Mutation>]
public sealed class LogoutMutation
{
    public async Task<MutationResult> Logout([Service] IAuthenticationService authenticationService)
    {
        await authenticationService.SignOut();
        return MutationResult.WithoutSubject();
    }
}
