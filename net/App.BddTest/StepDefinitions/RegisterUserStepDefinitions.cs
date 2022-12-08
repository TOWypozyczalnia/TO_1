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
    [Binding, Scope(Feature = "RegisterUser")]
    public class RegisterUserStepDefinitions
    {
        private static IConfigurationBuilder config = new ConfigurationBuilder().AddJsonFile("appsettings-Test.json");
        private static DbContextOptions<AppDbContext> options = new();

        private static AppDbContext dbContext;

        private static ILoggedUserRepository userRepo;

        private static HttpClient client;

        private string key;
        private LoggedUser userForCleanUp;

        private HttpResponseMessage response;

        [BeforeFeature]
        public static void Setup()
        {
            dbContext = new(config.Build(), options);

            userRepo = new LoggedUserRepository(dbContext);

            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>().WithWebHostBuilder(b => b.UseEnvironment("Test"));
            client = factory.CreateClient();
        }

        [When(@"I make POST request to /api/User/Register with body containing username string in json format")]
        public void WhenIMakePOSTRequestToApiUserRegisterWithBodyContainingUsernameStringInJsonFormat()
        {
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
            userForCleanUp = userRepo.GetAllAsync().ToList().Where(u => u.UserKey == key).ToList()!.First();
            Assert.Equal(key, userForCleanUp.UserKey);
        }

        [When(@"I make POST request to /api/User/Register with body containing wrong data")]
        public void WhenIMakePOSTRequestToApiUserRegisterWithBodyContainingWrongData()
        {
            string jsonData = "";
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            response = client.PostAsync("/api/User/Register", content).Result;
        }

        [Then(@"The response status code is BadRequest")]
        public void ThenTheResponseStatusCodeIsBadRequest()
        {
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
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