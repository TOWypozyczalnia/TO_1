using App.Data.Entities;
using App.Data.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class ReservationController : ControllerBase
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationController(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    [HttpPost("AddReservation")]
    public async Task<IActionResult> AddReservation([FromBody] Reservation reservation)
    {
        _reservationRepository.Add(reservation);
        return Ok();
    }
}