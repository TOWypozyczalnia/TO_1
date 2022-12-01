using App.Data.Entities;
using App.Data.Interfaces;

using Microsoft.AspNetCore.Mvc;

//using Microsoft.AspNetCore.Mvc.HttpGet;

namespace App.API.Controllers
{
    public class ActorController : BaseController
    {
        private readonly IActorRepository actorRepository;

        public ActorController(IActorRepository actorRepository)
        {
            this.actorRepository = actorRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            return new OkObjectResult(await actorRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingle(int Id)
        {
            CancellationToken cancellation = CancellationToken.None;
            return new OkObjectResult(await actorRepository.GetSingle(Id, cancellation));
        }

        [HttpPost("AddActor")]
        public async Task<IActionResult> AddActor([FromBody] Actor actor)
        {
            actorRepository.Add(actor);
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Actor actor)
        {
            actorRepository.Update(actor);
            return Ok();
        }

        [HttpPost("Remove")]
        public async Task<IActionResult> Remove([FromBody] Actor actor)
        {
            actorRepository.Remove(actor);
            return Ok();
        }
    }
}