namespace Shop.Application.Features.Catalog.OutputTypes;

public sealed record Product
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public required decimal UnitPrice { get; init; }
}
