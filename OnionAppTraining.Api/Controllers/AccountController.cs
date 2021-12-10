using Microsoft.AspNetCore.Mvc;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.User;
using System.Threading.Tasks;

namespace OnionAppTraining.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        public AccountController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpPut]
        [Route("{password}")]
        public async Task<IActionResult> Put([FromBody] ChangeUserPassowrd command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}
