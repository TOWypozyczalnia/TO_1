using App.Data.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories;

public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>
{
    protected readonly IAppDbContext AppDbContext;

    public BaseRepository(IAppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
    }

    public IQueryable<TEntity> GetAllAsync()
    {
        return AppDbContext.Set<TEntity, TKey>();
    }

    public async Task<TEntity> GetSingle(TKey id, CancellationToken cancellationToken)
    {
        return AppDbContext.Set<TEntity, TKey>().AsNoTracking().FirstOrDefault(d => d.Id.Equals(id));

        //return _appDbContext.Set<TEntity, TKey>().Where(x => x.Id!.Equals(id)).FirstOrDefault();
    }

    public void Add(TEntity entity)
    {
        var entityEntry = AppDbContext.Set<TEntity, TKey>().Add(entity);
        AppDbContext.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        var entityEntry = AppDbContext.Set<TEntity, TKey>().Update(entity);
        AppDbContext.SaveChanges();
    }

    public void Remove(TEntity entity)
    {
        AppDbContext.Set<TEntity, TKey>().Remove(entity);
        AppDbContext.SaveChanges();
    }
}