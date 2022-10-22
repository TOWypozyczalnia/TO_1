using App.Data.Interfaces;

namespace App.Data.Entities;

public class Director : IBaseEntity<string>
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}