using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.Drivers;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Handlers.Drivers
{
    public class DeleteDriverHandler : ICommandHandler<DeleteDriver>
    {
        private readonly IDriverService _driverService;

        public DeleteDriverHandler(IDriverService driverService)
        {
            _driverService = driverService;
        }

        public async Task HandleAsync(DeleteDriver command)
        {
            await _driverService.DeleteAsync(command.UserId);
        }
    }
}
