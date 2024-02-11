namespace Shop.Application.Features.Catalog.Entities;

internal sealed record Product
{
    public const int MaxNameLength = 200;

    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required decimal UnitPrice { get; init; }
}

internal sealed class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Product.MaxNameLength);

        builder.HasIndex(x => x.Name)
            .IsUnique(false);
    }
}
