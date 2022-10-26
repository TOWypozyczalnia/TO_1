using System.ComponentModel.DataAnnotations;
using App.Data.Interfaces;

namespace App.Data.Entities;

public class Actor : IBaseEntity<int>
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}