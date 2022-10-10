using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

public class MoviesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return new OkResult();
    }
}