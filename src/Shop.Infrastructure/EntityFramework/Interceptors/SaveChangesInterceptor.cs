using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Shop.Infrastructure.EntityFramework.Interceptors;

internal sealed class SaveChangesInterceptor : ISaveChangesInterceptor
{
    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        throw new NotImplementedException();
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        foreach (var entry in eventData.Context?.ChangeTracker.Entries() ?? Enumerable.Empty<EntityEntry>())
        {
            if (entry.State is EntityState.Modified && entry.Entity is Entity updatedEntity)
                updatedEntity.UpdatedAtUtc = DateTime.UtcNow;
            else if (entry.State is EntityState.Added && entry.Entity is Entity addedEntity)
                addedEntity.CreatedAtUtc = DateTime.UtcNow;
            else if (entry.State is EntityState.Deleted && entry.Entity is SoftDeletableEntity deletedEntity)
                deletedEntity.DeletedAtUtc = DateTime.UtcNow;
        }

        return ValueTask.FromResult(result);
    }
}
