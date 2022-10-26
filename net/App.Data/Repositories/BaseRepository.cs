using App.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories;

public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>
{
    private readonly IAppDbContext _appDbContext;

    public BaseRepository(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await _appDbContext.Set<TEntity, TKey>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetSingle(TKey id, CancellationToken cancellationToken)
    {
        return await _appDbContext.Set<TEntity, TKey>().AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public TEntity Add(TEntity entity)
    {
        var entityEntry = _appDbContext.Set<TEntity, TKey>().Add(entity);
        _appDbContext.SaveChanges();
        return entityEntry.Entity;
    }

    public TEntity Update(TEntity entity)
    {
        var entityEntry = _appDbContext.Set<TEntity, TKey>().Update(entity);
        _appDbContext.SaveChanges();
        return entityEntry.Entity;
    }

    public TEntity Remove(TEntity entity)
    {
        var entityEntry = _appDbContext.Set<TEntity, TKey>().Remove(entity);
        _appDbContext.SaveChanges();
        return entityEntry.Entity;
    }
}