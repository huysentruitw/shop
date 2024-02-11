namespace Shop.Application.Features.Organization.Entities;

internal sealed record User : Entity<Guid>
{
    public const int MaxEmailLength = 200;
    public const int MaxPasswordHashLength = 255;
    public const int MaxFirstNameLength = 100;
    public const int MaxLastNameLength = 100;

    public required string Email { get; init; }

    public required bool EmailConfirmed { get; init; }

    public required string PasswordHash { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required bool AcceptsTermsAndConditions { get; init; }

    #region Navigation Properties

    public ICollection<Organization> Organizations { get; init; } = null!;

    #endregion
}

internal sealed class UserEntityTypeConfiguration
    : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(User.MaxEmailLength);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.EmailConfirmed)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(User.MaxPasswordHashLength);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(User.MaxFirstNameLength);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(User.MaxLastNameLength);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired()
            .HasConversion(x => x, x => DateTime.SpecifyKind(x, DateTimeKind.Utc));

        builder.Property(x => x.UpdatedAtUtc)
            .HasConversion(x => x, x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : null);
    }
}
