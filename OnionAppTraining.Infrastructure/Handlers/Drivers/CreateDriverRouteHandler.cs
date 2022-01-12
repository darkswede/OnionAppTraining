using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.Drivers;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Handlers.Drivers
{
    public class CreateDriverRouteHandler : ICommandHandler<CreateDriverRoute>
    {
        private readonly IDriverRouteService _driverRouteService;

        public CreateDriverRouteHandler(IDriverRouteService driverRouteService)
        {
            _driverRouteService = driverRouteService;
        }

        public async Task HandleAsync(CreateDriverRoute command)
        {
            await _driverRouteService.AddAsync(command.UserId, command.Name, command.StartLongitude, command.StartLatitude, command.EndLongitude, command.EndLatitude);
        }
    }
}
