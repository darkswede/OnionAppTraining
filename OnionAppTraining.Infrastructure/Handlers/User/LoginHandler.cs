using Microsoft.Extensions.Caching.Memory;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.User;
using OnionAppTraining.Infrastructure.Extensions;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Handlers.User
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _memoryCache;

        public LoginHandler(IUserService userService, IJwtHandler jwtHandler, IMemoryCache memoryCache)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _memoryCache = memoryCache;
        }

        public async Task HandleAsync(Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetByEmailAsync(command.Email);
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
            _memoryCache.SetJwt(command.TokentId, jwt);
        }
    }
}
