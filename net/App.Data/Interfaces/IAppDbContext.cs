using Microsoft.EntityFrameworkCore;

namespace App.Data.Interfaces;

public interface IAppDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<TEntity> Set<TEntity, TKey>() where TEntity : class, IBaseEntity<TKey>;
}