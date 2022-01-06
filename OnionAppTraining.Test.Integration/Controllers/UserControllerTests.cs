using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using OnionAppTraining.Api;
using OnionAppTraining.Infrastructure.Commands.User;
using OnionAppTraining.Infrastructure.DTO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnionAppTraining.Test.Integration.Controllers
{
    public class UserControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        //public UserControllerTests()
        //{
        //    _server = new TestServer(new WebHostBuilder()
        //        .UseStartup<Startup>());
        //    _client = _server.CreateClient();
        //}

        [Fact]
        public async Task Handle_WhenValidDataProvided_ShouldReturnUser()
        {
            var email = "user1@gmail.com";

            var user = await GetUserAsync(email);

            user.Email.Should().BeEquivalentTo(email);
        }

        [Fact]
        public async Task Handle_WhenInvalidDataProvided_ShouldReturnNoData()
        {
            var email = "testUser13456@gmail.com";

            var reponse = await _client.GetAsync($"user/{email}");

            reponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task _WhenValidDataProvided_ShouldCreateUser()
        {
            var requestData = new CreateUser
            {
                Email = "testUser95@gmail.com",
                Password = "secret95",
                Username = "user95"
            };
            var payload = GetPayload(requestData);

            var reponse = await _client.PostAsync("user", payload);

            reponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            reponse.Headers.Location.ToString().Should().BeEquivalentTo($"user/{requestData.Email}");
            var user = await GetUserAsync(requestData.Email);
            user.Email.Should().BeEquivalentTo(requestData.Email);
        }

        private async Task<UserDTO> GetUserAsync(string email)
        {
            var reponse = await _client.GetAsync($"user/{email}");
            var reponseString = await reponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDTO>(reponseString);
        }

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
