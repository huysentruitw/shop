namespace Shop.Application.Exceptions;

internal sealed class InitialAccountCannotBeSetWhileLoggedInException : Exception
{
    public InitialAccountCannotBeSetWhileLoggedInException()
        : base("Initial account cannot be set while logged in. Leave InitialAccount empty to link the organization to your current account or log out first.")
    {
    }
}
