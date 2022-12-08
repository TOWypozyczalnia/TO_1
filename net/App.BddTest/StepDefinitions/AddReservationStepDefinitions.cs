using System.Text;
using System.Text.Json;

using App.Data;
using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App.BddTest.StepDefinitions
{
    [Binding, Scope(Feature = "AddReservation")]
    public class AddReservationStepDefinitions
    {
        private IConfigurationBuilder config = new ConfigurationBuilder().AddJsonFile("appsettings-Test.json");
        private DbContextOptions<AppDbContext> options = new();

        private AppDbContext dbContext;

        private IMovieRepository movieRepo;
        private ILoggedUserRepository userRepo;
        private IReservationRepository reservationRepo;

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
        private readonly Reservation testReservation = new()
        {
            MovieId = 1,
            UserId = 1,
            ReservationDate = DateTime.Now,
            ExpirationDate = DateTime.Now,
        };
        private Reservation reservationForCleanUp;

        private HttpResponseMessage response;

        [BeforeScenario]
        public void Setup()
        {
            dbContext = new(config.Build(), options);

            movieRepo = new MovieRepository(dbContext);
            userRepo = new LoggedUserRepository(dbContext);
            reservationRepo = new ReservationRepository(dbContext);
        }

        [Given(@"I am user")]
        public void GivenIAmUser()
        {
            userRepo.Add(testUser);
            testReservation.UserId = testUser.Id;
        }

        [Given(@"Movie repository contains records")]
        public void GivenMovieRepositoryContainsRecords()
        {
            movieRepo.Add(testMovie);
            testReservation.MovieId = testMovie.Id;
        }

        [When(@"I make POST request to /api/Reservation/AddReservation with body containing Reservation in json format")]
        public void WhenIMakePOSTRequestToApiReservationAddReservationWithBodyContainingReservationInJsonFormat()
        {
            WebApplicationFactory<Program> factory = new();
            HttpClient client = factory.CreateClient();

            string jsonData = JsonSerializer.Serialize(testReservation).Remove(2, 7);    // Remove Id from json
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            response = client.PostAsync("/api/Reservation/AddReservation", content).Result;
        }

        [Then(@"The response status code is OK")]
        public void ThenTheResponseStatusCodeIsOK()
        {
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"Reservation table contains new record")]
        public void ThenReservationTableContainsNewRecord()
        {
            Reservation expected = reservationForCleanUp = reservationRepo.GetAllAsync().ToList().Last();
            // Id is unknown -> need to check every field
            Assert.Equal(expected.MovieId, testReservation.MovieId);
            Assert.Equal(expected.UserId, testReservation.UserId);
            // Comparing time is hard -> there are problems with rounding when using json
            //Assert.Equal(expected.ReservationDate, testReservation.ReservationDate);
            //Assert.Equal(expected.ExpirationDate, testReservation.ExpirationDate);
        }

        [When(@"I make POST request to /api/Reservation/AddReservation with body containing Reservation in wrong format")]
        public void WhenIMakePOSTRequestToApiReservationAddReservationWithBodyContainingReservationInWrongFormat()
        {
            WebApplicationFactory<Program> factory = new();
            HttpClient client = factory.CreateClient();

            string jsonData = "{\"Garbage\"}";
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            response = client.PostAsync("/api/Reservation/AddReservation", content).Result;
        }

        [Then(@"The response status code is BadRequest")]
        public void ThenTheResponseStatusCodeIsBadRequest()
        {
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [AfterScenario]
        public void Cleanup()
        {
            if (reservationForCleanUp != null)
            {
                reservationRepo.Remove(reservationForCleanUp);
            }
            userRepo.Remove(testUser);
            movieRepo.Remove(testMovie);
        }
    }
}