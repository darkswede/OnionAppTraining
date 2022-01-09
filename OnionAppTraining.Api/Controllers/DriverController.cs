using Microsoft.AspNetCore.Mvc;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.Drivers;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Api.Controllers
{
    public class DriverController : ApiControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var drivers = await _driverService.GetAllDrivers();

            return Json(drivers);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDriver command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
