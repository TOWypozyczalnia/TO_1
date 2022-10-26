using System.ComponentModel.DataAnnotations;
using App.Data.Interfaces;

namespace App.Data.Entities;

public class Director : IBaseEntity<int>
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}