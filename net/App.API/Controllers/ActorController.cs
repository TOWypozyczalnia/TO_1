using Microsoft.AspNetCore.Mvc;
using App.Data.Repositories;
using App.Data.Entities;
using App.Data.Interfaces;
//using Microsoft.AspNetCore.Mvc.HttpGet;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace App.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ActorController : BaseController
    {
        private readonly IActorRepository actorRepository;

        public ActorController(IActorRepository actorRepository)
        {
            this.actorRepository = actorRepository;
        }
        
        
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult> GetAll()
        {
            return new OkObjectResult(await actorRepository.GetAllAsync());
        }

        //[HttpGet("{id}")]
        [HttpGet]
        [Route("getsingle")]
        public async Task<ActionResult> GetSingle(int Id)
        {
            CancellationToken cancellation = CancellationToken.None;
            return new OkObjectResult(await actorRepository.GetSingle(Id,cancellation));
        }
        [Route("addactor")]
        [HttpPost]
        public async Task<IActionResult>AddActor([FromBody] Actor actor)
        {
            Actor temp = actorRepository.Add(actor);
            return Ok(temp);
        }
        [Route("update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Actor actor)
        {
            Actor temp = actorRepository.Update(actor);
            return Ok(temp);
        }
        [Route("remove")]
        [HttpPost]
        public async Task<IActionResult> Remove([FromBody] Actor actor)
        {
            Actor temp = actorRepository.Remove(actor);
            return Ok(temp);
        }
        
    }
}
