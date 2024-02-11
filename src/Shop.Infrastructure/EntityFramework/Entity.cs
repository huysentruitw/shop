namespace Shop.Infrastructure.EntityFramework;

public abstract record Entity
{
    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }
}

public abstract record Entity<TId> : Entity
    where TId : struct
{
    public TId Id { get; init; }
}

public abstract record SoftDeletableEntity : Entity
{
    public DateTime? DeletedAtUtc { get; set; }
}

public abstract record SoftDeletableEntity<TId> : SoftDeletableEntity
    where TId : struct
{
    public TId Id { get; init; }
}
