using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.API.Controllers;
using App.Data.Entities;
using App.Data.Interfaces;
using FakeItEasy;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace App.Test.Controllers
{
    public class ActorControllerTest
    {
        private readonly IActorRepository _actorRepository;

        public ActorControllerTest()
        {
            _actorRepository = A.Fake<IActorRepository>();
        }

        [Fact]
        public async void ActorController_GetAll_ReturnOk()
        {

            //Arrange
            var controller = new ActorController(_actorRepository);
            //Act
            var result = await controller.GetAll();
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public async void ActorController_GetSingle_ReturnOk()
        {
            //Arrange
            var id = 2;
            var controller = new ActorController(_actorRepository);

            //Act
            var result = await controller.GetSingle(2);

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }

        

        [Fact]
        public async void ActorController_AddActor_ReturnOk()
        {
            //Arrange
            var controller = new ActorController(_actorRepository);
            Actor actor = new Actor();
            //Act
            var result = await controller.AddActor(actor);
            //Assert
            Assert.IsType<OkObjectResult>(result);

        }



        [Fact]
        public async void ActorController_Remove_ReturnOk()
        {
            //Arrange
            var controller = new ActorController(_actorRepository);
            Actor actor = new Actor();
            //Act
            var result = await controller.Remove(actor);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void ActorController_Update_ReturnOk()
        {
            //Arrange
            var controller = new ActorController(_actorRepository);
            Actor actor = new Actor();
            //Act
            var result = await controller.Update(actor);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
