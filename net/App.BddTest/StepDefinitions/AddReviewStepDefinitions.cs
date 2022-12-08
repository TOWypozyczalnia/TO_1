using System.Text;
using System.Text.Json;

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
    [Binding, Scope(Feature = "AddReview")]
    public class AddReviewStepDefinitions
    {
        private static IConfigurationBuilder config = new ConfigurationBuilder().AddJsonFile("appsettings-Test.json");
        private static DbContextOptions<AppDbContext> options = new();
                 
        private static AppDbContext dbContext;

        private static IMovieRepository movieRepo;
        private static ILoggedUserRepository userRepo;
        private static IReviewRepository reviewRepo;

        private static HttpClient client;

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

        [BeforeFeature]
        public static void Setup()
        {
            dbContext = new(config.Build(), options);

            movieRepo = new MovieRepository(dbContext);
            userRepo = new LoggedUserRepository(dbContext);
            reviewRepo = new ReviewRepository(dbContext);

            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>().WithWebHostBuilder(b => b.UseEnvironment("Test"));
            client = factory.CreateClient();
        }

        [Given(@"I am user")]
        public void GivenIAmUser()
        {
            userRepo.Add(testUser);
            testReview.UserId = testUser.Id;
        }

        [Given(@"Movie repository contains records")]
        public void GivenMovieRepositoryContainsRecords()
        {
            movieRepo.Add(testMovie);
            testReview.MovieId = testMovie.Id;
        }

        [When(@"I make POST request to /api/Review/AddReview with body containing Review in json format")]
        public void WhenIMakePOSTRequestToApiReviewAddReviewWithBodyContainingReviewInJsonFormat()
        {
            string jsonData = JsonSerializer.Serialize(testReview).Remove(2, 7);    // Remove Id from json
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            response = client.PostAsync("/api/Review/AddReview", content).Result;
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

        [When(@"I make POST request to /api/Review/AddReview with body containing Review in wrong format")]
        public void WhenIMakePOSTRequestToApiReviewAddReviewWithBodyContainingReviewInWrongFormat()
        {
            string jsonData = "{\"Garbage\"}";
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            response = client.PostAsync("/api/Review/AddReview", content).Result;
        }

        [Then(@"The response status code is BadRequest")]
        public void ThenTheResponseStatusCodeIsBadRequest()
        {
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [AfterScenario]
        public void Cleanup()
        {
            if (reviewForCleanUp != null)
            {
                reviewRepo.Remove(reviewForCleanUp);
            }
            userRepo.Remove(testUser);
            movieRepo.Remove(testMovie);
        }
    }
}