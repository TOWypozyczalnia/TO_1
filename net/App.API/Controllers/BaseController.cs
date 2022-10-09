using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase {}