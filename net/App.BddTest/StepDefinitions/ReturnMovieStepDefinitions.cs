using System.Text;

using App.Data;
using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App.BddTest.StepDefinitions
{
    [Binding, Scope(Feature = "ReturnMovie")]
    public class ReturnMovieStepDefinitions
    {
        private static IConfigurationBuilder config = new ConfigurationBuilder().AddJsonFile("appsettings-Test.json");
        private static DbContextOptions<AppDbContext> options = new();

        private static AppDbContext dbContext;

        private static IMovieRepository movieRepo;

        private readonly Movie testMovie = new()
        {
            Name = "TestMovie",
            ProductionYear = DateTime.Now,
            BoxOffice = 0,
            Unavailable = 1
        };

        private static HttpClient client;
        private HttpResponseMessage response;
        
        [BeforeFeature]
        public static void Setup()
        {
            dbContext = new(config.Build(), options);

            movieRepo = new MovieRepository(dbContext);

            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>().WithWebHostBuilder(b => b.UseEnvironment("Test"));
            client = factory.CreateClient();
        }

        [Given(@"Movie repository contains records")]
        public void GivenMovieRepositoryContainsRecords()
        {
            movieRepo.Add(testMovie);
        }

        [When(@"I make POST request to /api/Employee/Return with body containing id of movie in json format")]
        public void WhenIMakePOSTRequestToApiEmployeeReturnWithBodyContainingIdOfMovieInJsonFormat()
        {
            string jsonData = testMovie.Id.ToString();
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            response = client.PostAsync("/api/Employee/Return", content).Result;
        }

        [Then(@"The response status code is OK")]
        public void ThenTheResponseStatusCodeIsOK()
        {
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"Movie with given id is updated in table")]
        public void ThenMovieWithGivenIdIsUpdatedInTable()
        {
            Assert.Equal(0, movieRepo.GetSingle(testMovie.Id, new CancellationToken()).Result.Unavailable);
        }

        [When(@"I make POST request to /api/Employee/Return with empty body")]
        public void WhenIMakePOSTRequestToApiEmployeeReturnWithEmptyBody()
        {
            string jsonData = "";
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            response = client.PostAsync("/api/Employee/Return", content).Result;
        }

        [Then(@"The response status code is BadRequest")]
        public void ThenTheResponseStatusCodeIsBadRequest()
        {
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [AfterScenario]
        public void Cleanup()
        {
            movieRepo.Remove(testMovie);
        }
    }
}