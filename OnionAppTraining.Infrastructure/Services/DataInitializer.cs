﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly ILogger _logger;

        public DataInitializer()
        {
        }

        public DataInitializer(IUserService userService, IDriverService driverService, ILogger logger)
        {
            _userService = userService;
            _driverService = driverService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            _logger.LogTrace("Initializind data..");

            var tasks = new List<Task>();
            for(var i = 1; i <= 10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";
                var mail = $"{username}@gmail.com";
                var secret = $"secret{i}";
                tasks.Add(_userService.RegisterAsync(userId, mail, secret, "user", username));
                _logger.LogTrace($"created new user: {username}");

                tasks.Add(_driverService.CreateAsync(userId));
                tasks.Add(_driverService.SetVehicleAsync(userId, "LADA", "no LADA", 2));
                _logger.LogTrace($"created new driver: {username}");
            }

            for( var i = 1; i <= 3; i++)
            {
                var username = $"admin{i}";
                var mail = $"{username}@gmail.com";
                var secret = $"secret{i}";
                tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), mail, secret, "admin", username));
                _logger.LogTrace($"created new user: {username}");
            }
            await Task.WhenAll(tasks);

            _logger.LogTrace("Data initialized.");
        }
    }
}