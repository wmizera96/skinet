using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DbSetExtensions
{
    public static void SoftDelete<T>(this DbSet<T> dbSet, IStronglyTypedId<T> entityId) where T : BaseEntity
    {
        var entity = entityId.CreateEmptyEntity();
        var entry = dbSet.Attach(entity);
        entry.State = EntityState.Deleted;
    }
}