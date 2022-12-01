using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public abstract class BaseController : ControllerBase { }