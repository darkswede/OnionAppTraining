using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public DataInitializer()
        {
        }

        public DataInitializer(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            _logger.LogTrace("Initializind data..");

            var tasks = new List<Task>();
            for(var i = 1; i <= 10; i++)
            {
                var username = $"user{i}";
                var mail = $"{username}@gmail.com";
                var secret = $"secret{i}";
                tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), mail, secret, "user", username));
                _logger.LogTrace("created new user: {username}");
            }

            for( var i = 1; i <= 3; i++)
            {
                var username = $"admin{i}";
                var mail = $"{username}@gmail.com";
                var secret = $"secret{i}";
                tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), mail, secret, "admin", username));
                _logger.LogTrace("created new user: {username}");
            }
            await Task.WhenAll(tasks);

            _logger.LogTrace("Data initialized.");
        }
    }
}
