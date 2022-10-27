using App.Data.Entities;
using App.Data.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

public class MoviesController : BaseController
{
    private readonly IMovieRepository _movieRepository;

    public MoviesController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll()
    {
        return new OkObjectResult(await _movieRepository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(int Id)
    {
        CancellationToken cancellation = CancellationToken.None;
        return new OkObjectResult(await _movieRepository.GetSingle(Id, cancellation));
    }

    [HttpPost("AddMovie")]
    public async Task<IActionResult> AddMovie([FromBody] Movie Movie)
    {
        Movie temp = _movieRepository.Add(Movie);
        return Ok(temp);
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(Movie Movie)
    {
        Movie temp = _movieRepository.Update(Movie);
        return Ok(temp);
    }

    [HttpPost("Remove")]
    public async Task<IActionResult> Remove(Movie Movie)
    {
        Movie temp = _movieRepository.Remove(Movie);
        return Ok(temp);
    }
}