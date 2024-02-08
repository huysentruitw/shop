namespace Shop.Application.Features.Basket.OutputTypes;

public sealed record Basket
{
    public required Guid Id { get; init; }

    public required IEnumerable<Reservation> Reservations { get; init; }
}

public sealed record Reservation
{
    public required Guid ProductId { get; init; }

    public required int Quantity { get; init; }
}
