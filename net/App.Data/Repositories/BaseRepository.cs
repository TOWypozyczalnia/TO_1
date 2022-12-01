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
        return _appDbContext.Set<TEntity, TKey>().AsNoTracking().FirstOrDefault(d => d.Id.Equals(id));

        //return _appDbContext.Set<TEntity, TKey>().Where(x => x.Id!.Equals(id)).FirstOrDefault();
    }

    public void Add(TEntity entity)
    {
        var entityEntry = _appDbContext.Set<TEntity, TKey>().Add(entity);
        _appDbContext.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        var entityEntry = _appDbContext.Set<TEntity, TKey>().Update(entity);
        _appDbContext.SaveChanges();
    }

    public void Remove(TEntity entity)
    {
        _appDbContext.Set<TEntity, TKey>().Remove(entity);
        _appDbContext.SaveChanges();
    }
}