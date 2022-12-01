using System.ComponentModel.DataAnnotations;

using App.Data.Interfaces;

namespace App.Data.Entities;

public class LoggedUser : IBaseEntity<int>
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string UserKey { get; set; }
    public int MoviesWatched { get; set; }
}