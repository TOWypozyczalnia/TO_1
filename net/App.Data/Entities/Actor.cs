using App.Data.Interfaces;

namespace App.Data.Entities;

public class Actor : IBaseEntity<string>
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}