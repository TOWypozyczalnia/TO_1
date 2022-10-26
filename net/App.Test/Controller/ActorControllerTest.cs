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

namespace App.Test.Controller
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
    }
}
