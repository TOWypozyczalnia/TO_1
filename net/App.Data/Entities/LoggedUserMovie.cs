using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Data.Entities;

public class LoggedUserMovie : IBaseEntity<int>
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    [NotMapped]
    public int Id { get; set; }
}