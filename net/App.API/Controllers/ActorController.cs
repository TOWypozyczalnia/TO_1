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
        
        
        [HttpPost]
        [Route("getall")]
        public async Task<ActionResult> GetAll()
        {
            return new OkObjectResult(await actorRepository.GetAllAsync<ActorResult, string>());
        }
        
        [HttpPost]
        [Route("getsingle")]
        public async Task<ActionResult> GetSingle(string Id)
        {
            CancellationToken cancellation = CancellationToken.None;
            return new OkObjectResult(await actorRepository.GetSingle<ActorResult, string>(Id,cancellation));
        }
        [Route("addactor")]
        [HttpPost]
        public async Task<IActionResult>AddActor(ActorResult actor)
        {
            ActorResult temp = actorRepository.Add<ActorResult, string>(actor);
            return Ok(temp);
        }
        [Route("update")]
        [HttpPost]
        public async Task<IActionResult> Update(ActorResult actor)
        {
            ActorResult temp = actorRepository.Update<ActorResult, string>(actor);
            return Ok(temp);
        }
        [Route("remove")]
        [HttpPost]
        public async Task<IActionResult> Remove(ActorResult actor)
        {
            ActorResult temp = actorRepository.Remove<ActorResult, string>(actor);
            return Ok(temp);
        }
        
    }
}
