namespace Shop.Application.Exceptions;

internal sealed class ProductNotFoundException : Exception
{
    public ProductNotFoundException(Guid productId)
        : base($"The product with id {productId} was not found")
    {
        ProductId = productId;
    }

    public Guid ProductId { get; }
}
