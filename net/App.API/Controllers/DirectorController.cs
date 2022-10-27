using App.Data.Entities;
using App.Data.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

public class DirectorController : BaseController
{
    private readonly IDirectorRepository _directorRepository;

    public DirectorController(IDirectorRepository directorRepository)
    {
        _directorRepository = directorRepository;
    }

    [HttpGet]
    [Route("getall")]
    public async Task<ActionResult> GetAll()
    {
        return new OkObjectResult(await _directorRepository.GetAllAsync());
    }

    [HttpGet]
    [Route("getsingle")]
    public async Task<ActionResult> GetSingle(int Id)
    {
        CancellationToken cancellation = CancellationToken.None;
        return new OkObjectResult(await _directorRepository.GetSingle(Id, cancellation));
    }

    [Route("add")]
    [HttpPost]
    public async Task<IActionResult> AddDirector([FromBody] Director director)
    {
        Director temp = _directorRepository.Add(director);
        return Ok(temp);
    }

    [Route("update")]
    [HttpPost]
    public async Task<IActionResult> Update(Director director)
    {
        Director temp = _directorRepository.Update(director);
        return Ok(temp);
    }

    [Route("remove")]
    [HttpPost]
    public async Task<IActionResult> Remove(Director director)
    {
        Director temp = _directorRepository.Remove(director);
        return Ok(temp);
    }
}