namespace Shop.Application.Exceptions;

internal sealed class BasketNotFoundException : Exception
{
    public BasketNotFoundException(Guid basketId)
        : base($"The basket with id {basketId} was not found")
    {
        BasketId = basketId;
    }

    public Guid BasketId { get; }
}
