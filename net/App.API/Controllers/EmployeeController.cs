using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class EmployeeController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public EmployeeController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpPost("Borrow")]
    public async Task<IActionResult> Borrow([FromBody] int movieId)
    {
        Movie movie = _movieRepository.GetSingle(movieId, new CancellationToken()).Result;
        movie.Unavailable = 1;
        _movieRepository.Update(movie);

        return Ok();
    }

    [HttpPost("Return")]
    public async Task<IActionResult> Return([FromBody] int movieId)
    {
        Movie movie = _movieRepository.GetSingle(movieId, new CancellationToken()).Result;
        movie.Unavailable = 0;
        _movieRepository.Update(movie);

        return Ok();
    }
}