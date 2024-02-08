namespace Shop.Application.Exceptions;

internal sealed class BasketReservationNotFoundException : Exception
{
    public BasketReservationNotFoundException(Guid productId)
        : base($"The basket reservation for product with id {productId} was not found")
    {
        ProductId = productId;
    }

    public Guid ProductId { get; }
}
