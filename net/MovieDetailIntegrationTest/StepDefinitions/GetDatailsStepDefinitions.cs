using System.Text;
using System.Text.Json;

using App.Data;
using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MovieDetailIntegrationTest.StepDefinitions
{
    [Binding]
    public class GetDatailsStepDefinitions
    {
        private IConfigurationBuilder config = new ConfigurationBuilder().AddJsonFile("appsettings-Test.json");
        private DbContextOptions<AppDbContext> options = new();

        private AppDbContext dbContext;

        private IMovieRepository movieRepository;

        private readonly Movie testMovie = new()
        {
            Id = 1,
            Name = "TestMovie",
            ProductionYear = DateTime.Now,
            BoxOffice = 0
        };

        private HttpResponseMessage response;

        [BeforeScenario]
        public void Setup()
        {
            dbContext = new(config.Build(), options);
            movieRepository = new MovieRepository(dbContext);
        }

        [Given(@"Movie repository contains records")]
        public void GivenMovieRepositoryContainsRecords()
        {
            movieRepository.Add(testMovie);
        }

        [When(@"I make GET request to /api/Movie/GetSingle")]
        public void WhenIMakeGETRequestToApiMovieGetAll()
        {
            HttpClient client = new();
            response = client.GetAsync("http://localhost:8081/api/Review/GetSingle{1}").Result;
        }

        [Then(@"The response status code is OK")]
        public void ThenTheResponseStatusCodeIsOK()
        {
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [AfterScenario]
        public void Cleanup()
        {
            movieRepository.Remove(testMovie);
        }


    }
}
