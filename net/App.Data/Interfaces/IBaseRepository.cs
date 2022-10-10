namespace App.Data.Interfaces;

public interface IBaseRepository
{
    Task<ICollection<TEntity>> GetAllAsync<TEntity, TKey>() where TEntity : class, IBaseEntity<TKey>;
    Task<TEntity> GetSingle<TEntity, TKey>(TKey id, CancellationToken cancellationToken) where TEntity : class, IBaseEntity<TKey>;
}