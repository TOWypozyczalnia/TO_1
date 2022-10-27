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

    [HttpGet]
    [Route("getall")]
    public async Task<ActionResult> GetAll()
    {
        return new OkObjectResult(await _movieRepository.GetAllAsync());
    }

    [HttpGet]
    [Route("getsingle")]
    public async Task<ActionResult> GetSingle(int Id)
    {
        CancellationToken cancellation = CancellationToken.None;
        return new OkObjectResult(await _movieRepository.GetSingle(Id, cancellation));
    }

    [Route("add")]
    [HttpPost]
    public async Task<IActionResult> AddMovie([FromBody] Movie Movie)
    {
        Movie temp = _movieRepository.Add(Movie);
        return Ok(temp);
    }

    [Route("update")]
    [HttpPost]
    public async Task<IActionResult> Update(Movie Movie)
    {
        Movie temp = _movieRepository.Update(Movie);
        return Ok(temp);
    }

    [Route("remove")]
    [HttpPost]
    public async Task<IActionResult> Remove(Movie Movie)
    {
        Movie temp = _movieRepository.Remove(Movie);
        return Ok(temp);
    }
}