namespace Shop.Application.Features.Organization.Entities;

internal sealed record Organization : SoftDeletableEntity<Guid>
{
    public const int MaxNameLength = 100;
    public const int MaxStreetAddressLength = 100;
    public const int MaxCityLength = 100;
    public const int MaxPostalCodeLength = 10;
    public const int MaxCountryCodeLength = 2;
    public const int MaxUrlLength = 100;

    public required string Name { get; init; }

    public required string StreetAddress { get; init; }

    public required string City { get; init; }

    public required string PostalCode { get; init; }

    public required string CountryCode { get; init; }

    public required Uri? Url { get; init; }

    public required CompanyInformation CompanyInformation { get; init; }

    #region Navigation Properties

    public ICollection<OrganizationUser> OrganizationUsers { get; init; } = null!;

    #endregion
}

internal sealed record CompanyInformation
{
    public string? BusinessName { get; init; }

    public string? StreetAddress { get; init; }

    public string? City { get; init; }

    public string? PostalCode { get; init; }

    public string? CountryCode { get; init; }

    public string? VatNumber { get; init; }
}

internal sealed class OrganizationEntityTypeConfiguration
    : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Organization.MaxNameLength);

        builder.HasIndex(x => x.Name)
            .IsUnique(false);

        builder.Property(x => x.StreetAddress)
            .IsRequired()
            .HasMaxLength(Organization.MaxStreetAddressLength);

        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(Organization.MaxCityLength);

        builder.Property(x => x.PostalCode)
            .IsRequired()
            .HasMaxLength(Organization.MaxPostalCodeLength);

        builder.HasIndex(x => x.PostalCode)
            .IsUnique(false);

        builder.Property(x => x.CountryCode)
            .IsRequired()
            .HasMaxLength(Organization.MaxCountryCodeLength);

        builder.Property(x => x.Url)
            .HasMaxLength(Organization.MaxUrlLength);

        builder.OwnsOne(x => x.CompanyInformation, options => options.ToJson());

        builder.HasMany(x => x.OrganizationUsers)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
