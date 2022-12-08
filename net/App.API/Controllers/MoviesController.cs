using App.Data.Entities;
using App.Data.Interfaces;
using App.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

public class MoviesController : BaseController
{
    private readonly IMovieRepository _movieRepository;
    private readonly IRecommendationSerivce _recommendationService;

    public MoviesController(IMovieRepository movieRepository, IRecommendationSerivce recommendationService)
    {
        _movieRepository = movieRepository;
        _recommendationService = recommendationService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll()
    {
        return new OkObjectResult(_movieRepository.GetAllAsync());
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
        _movieRepository.Add(Movie);
        return Ok();
    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update(Movie Movie)
    {
        _movieRepository.Update(Movie);
        return Ok();
    }

    [HttpPost("Remove")]
    public async Task<IActionResult> Remove(Movie Movie)
    {
        _movieRepository.Remove(Movie);
        return Ok();
    }

    [HttpGet("Recommended/{userId}")]
    public IActionResult GetRecommended(int userId)
    {
        return new OkObjectResult(_recommendationService.GetRandomRecommendation(userId));
    }
}