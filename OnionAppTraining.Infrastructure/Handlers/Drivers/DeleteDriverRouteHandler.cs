using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.Drivers;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Handlers.Drivers
{
    public class DeleteDriverRouteHandler : ICommandHandler<DeleteDriverRoute>
    {
        private readonly IDriverRouteService _driverRouteService;

        public DeleteDriverRouteHandler(IDriverRouteService driverRouteService)
        {
            _driverRouteService = driverRouteService;
        }

        public async Task HandleAsync(DeleteDriverRoute command)
        {
            await _driverRouteService.RemoveAsync(command.UserId, command.Name);
        }
    }
}
