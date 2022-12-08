using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Interfaces;

namespace App.Data.Entities;

public class UserSimilarity : IBaseEntity<int>
{
    [Key]
    public int UserId { get; set; }
    public int Count { get; set; }
    [NotMapped]
    public int Id { get; set; }
}