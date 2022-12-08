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
    public class RandomMovieServiceTest
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
            Movie movieResult = randomMovieService.DrawMovie();
            Assert.Equal(movieResult, movieToList);
            //Assert

        }
       
    }
}
