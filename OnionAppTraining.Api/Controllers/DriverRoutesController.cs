using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.Drivers;
using System.Threading.Tasks;

namespace OnionAppTraining.Api.Controllers
{
    [Route("driver/routes")]
    public class DriverRoutesController : ApiControllerBase
    {
        public DriverRoutesController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriverRoute command)
        {
            await DispatchAsync(command);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var command = new DeleteDriverRoute
            {
                Name = name
            };
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
