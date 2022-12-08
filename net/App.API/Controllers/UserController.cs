using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class UserController : ControllerBase
{
    private readonly ILoggedUserRepository _userRepository;

    public UserController(ILoggedUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] string username)
    {
        string key = Guid.NewGuid().ToString();

        LoggedUser user = new()
        {
            UserKey = key,
            Username = username,
            MoviesWatched = 0,
            Movies = new(),
        };

        _userRepository.Add(user); 

        return new OkObjectResult(key);
    }
}