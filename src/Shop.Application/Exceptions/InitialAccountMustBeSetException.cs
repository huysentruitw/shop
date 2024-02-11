namespace Shop.Application.Exceptions;

internal sealed class InitialAccountMustBeSetException : Exception
{
    public InitialAccountMustBeSetException()
        : base("Initial account must be set when not logged in.")
    {
    }
}
