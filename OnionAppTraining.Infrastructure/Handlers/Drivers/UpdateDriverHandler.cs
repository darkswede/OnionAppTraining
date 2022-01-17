using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.Drivers;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Handlers.Drivers
{
    public class UpdateDriverHandler : ICommandHandler<UpdateDriver>
    {
        private readonly IDriverService _driverservice;

        public UpdateDriverHandler(IDriverService driverService)
        {
            _driverservice = driverService;
        }

        public async Task HandleAsync(UpdateDriver command)
        {
            await _driverservice.SetVehicleAsync(command.UserId, command.Vehicle.Brand, command.Vehicle.Name);
        }
    }
}
