using OnionAppTraining.Core.Domain;

namespace OnionAppTraining.Infrastructure.Commands.Drivers
{
    public class CreateDriver : AuthenticatedCommandBase
    {
        public Vehicle Vehicle { get; set; }
        public class DriverVehicle
        {
            public string Brand { get; set; }
            public string Name { get; set; }
        }
    }
}
