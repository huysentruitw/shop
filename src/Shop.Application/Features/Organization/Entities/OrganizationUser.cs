namespace Shop.Application.Features.Organization.Entities;

internal sealed record OrganizationUser : Entity
{
    public required Guid OrganizationId { get; init; }

    public required Guid UserId { get; init; }

    public required bool IsAdmin { get; init; }

    #region Navigation Properties

    public Organization Organization { get; init; } = null!;

    public User User { get; init; } = null!;

    #endregion
}

internal sealed class OrganizationUserEntityTypeConfiguration
    : IEntityTypeConfiguration<OrganizationUser>
{
    public void Configure(EntityTypeBuilder<OrganizationUser> builder)
    {
        builder.HasKey(x => new { x.OrganizationId, x.UserId });

        builder.HasOne(x => x.Organization)
            .WithMany(x => x.OrganizationUsers)
            .HasForeignKey(x => x.OrganizationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
