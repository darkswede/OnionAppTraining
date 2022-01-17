using OnionAppTraining.Infrastructure.Commands.Drivers.Models;

namespace OnionAppTraining.Infrastructure.Commands.Drivers
{
    public class UpdateDriver : AuthenticatedCommandBase
    {
        public DriverVehicle Vehicle { get; set; }
    }
}
