using App.API.Controllers;
using App.Data.Entities;
using App.Data.Interfaces;

using FakeItEasy;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace App.Test.Controllers
{
    public class UserControllerTest
    {
        private readonly ILoggedUserRepository _userRepository;

        public UserControllerTest()
        {
            _userRepository = A.Fake<ILoggedUserRepository>();
        }

        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("test3")]
        public async void UserController_Register_ReturnOk(string username)
        {
            //Arrange
            var controller = new UserController(_userRepository);
            //Act
            var result = await controller.Register(username);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}