using App.Data.Interfaces;

namespace App.Data.Entities;

public class Movie : IBaseEntity<string>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime ProductionYear { get; set; }
    public int BoxOffice { get; set; }
}