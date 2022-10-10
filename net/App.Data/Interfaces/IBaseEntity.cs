namespace App.Data.Interfaces;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
}