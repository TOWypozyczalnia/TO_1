using System.ComponentModel.DataAnnotations;
using App.Data.Interfaces;

namespace App.Data.Entities;

public class Movie : IBaseEntity<int>
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ProductionYear { get; set; }
    public int BoxOffice { get; set; }
}