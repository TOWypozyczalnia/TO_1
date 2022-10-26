namespace App.Data.Interfaces;

public interface IBaseRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task<TEntity> GetSingle(TKey id, CancellationToken cancellationToken);
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Remove(TEntity entity);

}