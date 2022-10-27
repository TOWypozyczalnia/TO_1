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
    public class MoviesControllerTest
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesControllerTest()
        {
            _movieRepository = A.Fake<IMovieRepository>();
        }

        [Fact]
        public async void MoviesController_GetAll_ReturnOk()
        {

            //Arrange
            var controller = new MoviesController(_movieRepository);
            //Act
            var result = await controller.GetAll();
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void MoviesController_GetSingle_ReturnOk()
        {
            //Arrange
            var id = 2;
            var controller = new MoviesController(_movieRepository);

            //Act
            var result = await controller.GetSingle(2);

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }



        [Fact]
        public void MoviesController_AddActor_ReturnOk()
        {
            //Arrange
            var controller = new MoviesController(_movieRepository);
            Movie movie = new Movie();
            //Act
            var result = controller.AddMovie(movie);
            //Assert
            Assert.NotNull(result);
        }



        [Fact]
        public void MoviesController_Remove_ReturnOk()
        {
            //Arrange
            var controller = new MoviesController(_movieRepository);
            Movie movie = new Movie();
            //Act
            var result = controller.Remove(movie);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void MoviesController_Update_ReturnOk()
        {
            //Arrange
            var controller = new MoviesController(_movieRepository);
            Movie movie = new Movie();
            //Act
            var result = controller.Update(movie);
            //Assert
            Assert.NotNull(result);
        }

    }
}
