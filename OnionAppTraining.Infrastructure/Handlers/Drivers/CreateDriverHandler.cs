using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.Drivers;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Handlers.Drivers
{
    public class CreateDriverHandler : ICommandHandler<CreateDriver>
    {
        private readonly IDriverService _driverService;

        public CreateDriverHandler(IDriverService driverService)
        {
            _driverService = driverService;
        }

        public async Task HandleAsync(CreateDriver command)
        {
            await _driverService.CreateAsync(command.UserId);
            var vehicle = command.Vehicle;
            await _driverService.SetVehicleAsync(command.UserId, vehicle.Brand, vehicle.Name);
        }
    }
}
