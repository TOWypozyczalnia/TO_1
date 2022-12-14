using System.ComponentModel.DataAnnotations;

using App.Data.Interfaces;

namespace App.Data.Entities;

public class Review : IBaseEntity<int>
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public int Rating { get; set; }
}