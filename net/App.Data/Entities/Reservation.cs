using System.ComponentModel.DataAnnotations;

using App.Data.Interfaces;

namespace App.Data.Entities;

public class Reservation : IBaseEntity<int>
{
    [Key]
    public int Id { get; set; }
    public int UserId{ get; set; }
    public int MovieId { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}