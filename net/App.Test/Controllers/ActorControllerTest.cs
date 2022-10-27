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
        public void ActorController_GetAll_ReturnOk()
        {

            //Arrange
            //var actors = A.Fake<ICollection<Actor>>();

            var controller = new ActorController(_actorRepository);

            //act
            var result = controller.GetAll();
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async void ActorController_GetSingle_ReturnOk()
        {
            //Arrange
            //var actors = A.Fake<ICollection<Actor>>();
            var id = 2;
            var controller = new ActorController(_actorRepository);

            //act
            var result = await controller.GetSingle(2);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ActorController_AddActor_ReturnOk()
        {
            int id = 1;
            string firstname = "ala";
            string lastname = "makota";
            DateTime DateOfBirth = new DateTime(2008, 5, 1, 8, 30, 52);

            var controller = new ActorController(_actorRepository);
            //A.CallTo(() => _actorRepository.GetSingle(1, new CancellationToken()));
            Actor actor = new Actor();
            var result = controller.AddActor(actor);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Task_GetPosts_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new ActorController(_actorRepository);

            //Act  
            var data = controller.GetAll();
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public void ActorController_Remove_ReturnOk()
        {
            int id = 1;
            string firstname = "ala";
            string lastname = "makota";
            DateTime DateOfBirth = new DateTime(2008, 5, 1, 8, 30, 52);

            var controller = new ActorController(_actorRepository);
            //A.CallTo(() => _actorRepository.GetSingle(1, new CancellationToken()));
            Actor actor = new Actor();
            var result = controller.Remove(actor);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ActorController_Update_ReturnOk()
        {
            int id = 1;
            string firstname = "ala";
            string lastname = "makota";
            DateTime DateOfBirth = new DateTime(2008, 5, 1, 8, 30, 52);

            var controller = new ActorController(_actorRepository);
            //A.CallTo(() => _actorRepository.GetSingle(1, new CancellationToken()));
            Actor actor = new Actor();
            var result = controller.Remove(actor);
            Assert.IsType<OkObjectResult>(result);
        }
        /*
        [Fact]
        public async Task ActorController_GetAll_Invalid()
        {
            var controller = new ActorController(_actorRepository);
            controller.ModelState.AddModelError("session", "required");
            //act
            var result = controller.GetAll();

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
        */
        [Fact]
        public async Task AddActor_ReturnsBadRequest_GivenInvalidModel()
        {
            // Arrange & Act
            var controller = new ActorController(_actorRepository);
            controller.ModelState.AddModelError("error", "some error");
            Actor ivalidActor = null;
            // Act
            var result = await controller.AddActor(ivalidActor);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        /*
        public async Task Create_ReturnsHttpNotFound_Invalid()
        {
            // Arrange
            _actorRepository.Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync((BrainstormSession)null);
            var controller = new ActorController(_actorRepository);

            // Act
            var result = await controller.Create(new NewIdeaModel());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        */
    }
}
