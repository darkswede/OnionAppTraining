﻿using OnionAppTraining.Infrastructure.Commands.Drivers.Models;

namespace OnionAppTraining.Infrastructure.Commands.Drivers
{
    public class CreateDriver : AuthenticatedCommandBase
    {
        public DriverVehicle Vehicle { get; set; }
    }
}
