using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.Drivers;
using OnionAppTraining.Infrastructure.Services;
using System;
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
        [Route("userId")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var driver = await _driverService.GetDriverByIdAsync(userId);
            if (driver == null)
            {
                return NotFound();
            }

            return Json(driver);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var drivers = await _driverService.GetAllDrivers();

            return Json(drivers);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDriver command)
        {
            await DispatchAsync(command);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> Delete()
        {
            await DispatchAsync(new DeleteDriver());

            return NoContent();
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> Put([FromBody]UpdateDriver command)
        {
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
