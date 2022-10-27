using Microsoft.AspNetCore.Mvc;
using App.Data.Repositories;
using App.Data.Entities;
using App.Data.Interfaces;
//using Microsoft.AspNetCore.Mvc.HttpGet;
using Microsoft.AspNetCore.Components.Forms;
using System;

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
            return new OkObjectResult(await actorRepository.GetSingle(Id,cancellation));
        }
        [HttpPost("AddActor")]
        public async Task<IActionResult>AddActor([FromBody] Actor actor)
        {
            Actor temp = actorRepository.Add(actor);
            return Ok(temp);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Actor actor)
        {
            Actor temp = actorRepository.Update(actor);
            return Ok(temp);
        }
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove([FromBody] Actor actor)
        {
            Actor temp = actorRepository.Remove(actor);
            return Ok(temp);
        }
        
    }
}
