using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Application.Features.Basket.Entities;

internal sealed record Basket
{
    public required Guid Id { get; init; }

    public required ICollection<Reservation> Reservations { get; init; }
}

internal sealed record Reservation
{
    public required Guid ProductId { get; init; }

    public required int Quantity { get; set; }
}

internal sealed class BasketEntityTypeConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsMany<Reservation>(x => x.Reservations, options => options.ToJson());
    }
}
