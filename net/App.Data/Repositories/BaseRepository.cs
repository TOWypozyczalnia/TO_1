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
        return await _appDbContext.Set<TEntity, TKey>().ToListAsync();
    }

    public async Task<TEntity> GetSingle<TEntity, TKey>(TKey id, CancellationToken cancellationToken) where TEntity : class, IBaseEntity<TKey>
    {
        return await _appDbContext.Set<TEntity, TKey>().FirstOrDefaultAsync(cancellationToken);
    }
}