using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    private readonly UserProvider _userProvider;

    public SoftDeleteInterceptor(UserProvider userProvider)
    {
        _userProvider = userProvider;
    }
    
    
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SoftDeleteEntries(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        SoftDeleteEntries(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SoftDeleteEntries(DbContextEventData eventData)
    {
        if (eventData.Context is null)
            return;
        
        foreach (var entry in eventData.Context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Unchanged;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = DateTimeOffset.UtcNow;
                entry.Entity.DeletedBy = _userProvider.GetUser();

                entry.Property(x => x.IsDeleted).IsModified = true;
                entry.Property(x => x.DeletedAt).IsModified = true;
                entry.Property(x => x.DeletedBy).IsModified = true;
            }
        }
    }
}