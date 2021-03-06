using Microsoft.AspNetCore.Mvc;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.User;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IJwtHandler _jwtHandler;

        public AccountController(ICommandDispatcher commandDispatcher, IJwtHandler jwtHandler) : base(commandDispatcher)
        {
            _jwtHandler = jwtHandler;
        }

        [HttpPut]
        [Route("{password}")]
        public async Task<IActionResult> Put([FromBody] ChangeUserPassowrdCommand command)
        {
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
