using App.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Service;
using App.Data.Entities;

using Xunit;

namespace App.Test.Services
{
    public class RecommendationServiceTest
    {
        [Fact]
        public void DrawMovieTest()
        {
            //Arrange
            RandomMovieService randomMovieService = new RandomMovieService();
            List<Movie>movies = new List<Movie>();
            Movie movieToList = new Movie();
            //Act
            
            movies.Add(movieToList);
            Movie movieResult = randomMovieService.DrawMovie(movies);
            Assert.Equal(movieResult, movieToList);
            //Assert
        }
        [Fact]
        public void DrawMovieTest_ReturnNULL()
        {
            //Arrange
            RandomMovieService randomMovieService = new RandomMovieService();
            List<Movie> movies = new List<Movie>();
            //Act
            Movie movieResult = randomMovieService.DrawMovie(movies);
            Assert.Equal(movieResult, null);
            //Assert
        }
    }
}
