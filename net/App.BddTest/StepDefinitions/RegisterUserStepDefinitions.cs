using System.Text;

using App.Data;
using App.Data.Entities;
using App.Data.Interfaces;
using App.Data.Repositories;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App.BddTest.StepDefinitions
{
    [Binding]
    public class RegisterUserStepDefinitions
    {
        private IConfigurationBuilder config = new ConfigurationBuilder().AddJsonFile("appsettings-Test.json");
        private DbContextOptions<AppDbContext> options = new();

        private AppDbContext dbContext;

        private ILoggedUserRepository userRepo;

        private string key;
        private LoggedUser userForCleanUp;

        private HttpResponseMessage response;

        [BeforeScenario]
        public void Setup()
        {
            dbContext = new(config.Build(), options);

            userRepo = new LoggedUserRepository(dbContext);
        }

        [When(@"I make POST request to /api/User/Register with body containing username string in json format")]
        public void WhenIMakePOSTRequestToApiUserRegisterWithBodyContainingUsernameStringInJsonFormat()
        {
            WebApplicationFactory<Program> factory = new();
            HttpClient client = factory.CreateClient();

            string jsonData = "\"username\"";
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            response = client.PostAsync("/api/User/Register", content).Result;
        }

        [Then(@"The response status code is OK")]
        public void ThenTheResponseStatusCodeIsOK()
        {
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"The response contains user key")]
        public void ThenTheResponseContainsUserKey()
        {
            key = response.Content.ReadAsStringAsync().Result!;
            Assert.Equal(36, key.Length);   // Guid.NewGuid() has length of 36
        }

        [Then(@"LoggedUser table contains new record")]
        public void ThenLoggedUserTableContainsNewRecord()
        {
            userForCleanUp = userRepo.GetAllAsync().ToList().Last();
            Assert.Equal(key, userForCleanUp.UserKey);
        }

        [AfterScenario]
        public void Cleanup()
        {
            if (userForCleanUp != null)
            {
                userRepo.Remove(userForCleanUp);
            }
        }
    }
}