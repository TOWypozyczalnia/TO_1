using App.Data.Interfaces;

namespace App.Data.Entities;

public class Genre : IBaseEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
}