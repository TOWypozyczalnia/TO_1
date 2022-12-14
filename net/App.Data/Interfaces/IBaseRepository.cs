namespace App.Data.Interfaces;

public interface IBaseRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>
{
    IQueryable<TEntity> GetAllAsync();
    Task<TEntity> GetSingle(TKey id, CancellationToken cancellationToken);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}