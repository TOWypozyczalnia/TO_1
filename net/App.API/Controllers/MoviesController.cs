using App.Data.Entities;
using App.Data.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

public class MoviesController : BaseController
{
    private readonly IActorRepository _actorRepository;

    public MoviesController(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return new OkObjectResult(await _actorRepository.GetAllAsync<ActorResult, string>());
    }
}