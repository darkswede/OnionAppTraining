using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.User;
using OnionAppTraining.Infrastructure.Extensions;
using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache memoryCache) : base(commandDispatcher)
        {
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Post([FromBody] Login command)
        {
            command.TokentId = Guid.NewGuid();
            await CommandDispatcher.DispatchAsync(command);
            var jwt = _memoryCache.GetJwt(command.TokentId);

            return Json(jwt);
        }
    }
}
