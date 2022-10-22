using App.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories;

public abstract class BaseRepository : IBaseRepository
{
    private readonly IAppDbContext _appDbContext;

    public BaseRepository(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ICollection<TEntity>> GetAllAsync<TEntity, TKey>() where TEntity : class, IBaseEntity<TKey>
    {
        return await _appDbContext.Set<TEntity, TKey>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetSingle<TEntity, TKey>(TKey id, CancellationToken cancellationToken) where TEntity : class, IBaseEntity<TKey>
    {
        return await _appDbContext.Set<TEntity, TKey>().AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public TEntity Add<TEntity, TKey>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
    {
        var entityEntry = _appDbContext.Set<TEntity, TKey>().Add(entity);
        return entityEntry.Entity;
    }

    public TEntity Update<TEntity, TKey>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
    {
        var entityEntry = _appDbContext.Set<TEntity, TKey>().Update(entity);
        return entityEntry.Entity;
    }

    public TEntity Remove<TEntity, TKey>(TEntity entity) where TEntity : class, IBaseEntity<TKey>
    {
        var entityEntry = _appDbContext.Set<TEntity, TKey>().Remove(entity);
        return entityEntry.Entity;
    }
}