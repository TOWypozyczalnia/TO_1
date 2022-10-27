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
    }
}
