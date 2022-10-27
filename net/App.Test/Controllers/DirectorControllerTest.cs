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
    public class DirctorControllerTest
    {
        private readonly IDirectorRepository _directorRepository;

        public DirctorControllerTest()
        {
            _directorRepository = A.Fake<IDirectorRepository>();
        }

        [Fact]
        public async void DirctorController_GetAll_ReturnOk()
        {

            //Arrange
            var controller = new DirectorController(_directorRepository);
            //Act
            var result = await controller.GetAll();
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void DirctorController_GetSingle_ReturnOk()
        {
            //Arrange
            var id = 2;
            var controller = new DirectorController(_directorRepository);

            //Act
            var result = await controller.GetSingle(2);

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }



        [Fact]
        public async void DirctorController_AddActor_ReturnOk()
        {
            //Arrange
            var controller = new DirectorController(_directorRepository);
            Director director = new Director();
            //Act
            var result = await controller.AddDirector(director);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public async void DirctorController_Remove_ReturnOk()
        {
            //Arrange
            var controller = new DirectorController(_directorRepository);
            Director director = new Director();
            //Act
            var result = await controller.Remove(director);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void DirctorController_Update_ReturnOk()
        {
            //Arrange
            var controller = new DirectorController(_directorRepository);
            Director director = new Director();
            //Act
            var result = await controller.Update(director);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
