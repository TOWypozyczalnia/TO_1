using System.Text;
using System.Text.Json;

using App.Data;
using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App.BddTest.StepDefinitions
{
    [Binding]
    public class AddReviewStepDefinitions
    {
        private IConfigurationBuilder config = new ConfigurationBuilder().AddJsonFile("appsettings-Test.json");
        private DbContextOptions<AppDbContext> options = new();

        private AppDbContext dbContext;

        private IMovieRepository movieRepo;
        private ILoggedUserRepository userRepo;
        private IReviewRepository reviewRepo;

        private readonly Movie testMovie = new()
        {
            Name = "TestMovie",
            ProductionYear = DateTime.Now,
            BoxOffice = 0
        };
        private readonly LoggedUser testUser = new()
        {
            UserKey = Guid.NewGuid().ToString(),
            Username = "TestUser",
            MoviesWatched = 0
        };
        private readonly Review testReview = new()
        {
            MovieId = 1,
            UserId = 1,
            Rating = 0
        };
        private Review reviewForCleanUp;

        private HttpResponseMessage response;

        [BeforeScenario]
        public void Setup()
        {
            dbContext = new(config.Build(), options);

            movieRepo = new MovieRepository(dbContext);
            userRepo = new LoggedUserRepository(dbContext);
            reviewRepo = new ReviewRepository(dbContext);
        }

        [Given(@"I am user")]
        public void GivenIAmUser()
        {
            userRepo.Add(testUser);
        }

        [Given(@"Movie repository contains records")]
        public void GivenMovieRepositoryContainsRecords()
        {
            movieRepo.Add(testMovie);
        }

        [When(@"I make POST request to /api/Review/AddReview with body containing Review in json format")]
        public void WhenIMakePOSTRequestToApiReviewAddReviewWithBodyContainingReviewInJsonFormat()
        {
            HttpClient client = new();

            string jsonData = JsonSerializer.Serialize(testReview).Remove(2, 7);    // Remove Id from json
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            response = client.PostAsync("http://localhost:8081/api/Review/AddReview", content).Result;
        }

        [Then(@"The response status code is OK")]
        public void ThenTheResponseStatusCodeIsOK()
        {
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"Review table contains new record")]
        public void ThenReviewTableContainsNewRecord()
        {
            Review expected = reviewForCleanUp = reviewRepo.GetAllAsync().ToList().Last();
            // Id is unknown -> need to check every field
            Assert.Equal(expected.MovieId, testReview.MovieId);
            Assert.Equal(expected.UserId, testReview.UserId);
            Assert.Equal(expected.Rating, testReview.Rating);
        }

        [AfterScenario]
        public void Cleanup()
        {
            // Only works if there are already records in table. Otherwise there's problem with ForeignKeys
            //movieRepo.Remove(testMovie);
        }
    }
}