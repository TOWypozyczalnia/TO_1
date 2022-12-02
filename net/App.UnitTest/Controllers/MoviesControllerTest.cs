using App.API.Controllers;
using App.Data.Entities;
using App.Data.Interfaces;
using App.Service.Interfaces;
using FakeItEasy;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace App.Test.Controllers
{
    public class MoviesControllerTest
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IRecommendationSerivce _recommendationSerivce;

        public MoviesControllerTest()
        {
            _movieRepository = A.Fake<IMovieRepository>();
            _recommendationSerivce = A.Fake<IRecommendationSerivce>();
        }

        [Fact]
        public async void MoviesController_GetAll_ReturnOk()
        {
            //Arrange
            var controller = new MoviesController(_movieRepository, _recommendationSerivce);
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
            var controller = new MoviesController(_movieRepository, _recommendationSerivce);

            //Act
            var result = await controller.GetSingle(2);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void MoviesController_AddActor_ReturnOk()
        {
            //Arrange
            var controller = new MoviesController(_movieRepository, _recommendationSerivce);
            Movie movie = new Movie();
            //Act
            var result = await controller.AddMovie(movie);
            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void MoviesController_Remove_ReturnOk()
        {
            //Arrange
            var controller = new MoviesController(_movieRepository, _recommendationSerivce);
            Movie movie = new Movie();
            //Act
            var result = await controller.Remove(movie);
            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void MoviesController_Update_ReturnOk()
        {
            //Arrange
            var controller = new MoviesController(_movieRepository, _recommendationSerivce);
            Movie movie = new Movie();
            //Act
            var result = await controller.Update(movie);
            //Assert
            Assert.IsType<OkResult>(result);
        }
    }
}