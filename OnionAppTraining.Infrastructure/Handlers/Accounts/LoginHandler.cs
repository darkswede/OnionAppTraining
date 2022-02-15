using Microsoft.Extensions.Caching.Memory;
using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.User;
using OnionAppTraining.Infrastructure.Extensions;
using OnionAppTraining.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Handlers.Accounts
{
    public class LoginHandler : ICommandHandler<LoginCommand>
    {
        private readonly IUserService _userservice;
        private readonly IHandler _handler;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _memorycache;

        public LoginHandler(IUserService userService, IHandler handler, IJwtHandler jwtHandler, IMemoryCache memoryCache)
        {
            _userservice = userService;
            _handler = handler;
            _jwtHandler = jwtHandler;
            _memorycache = memoryCache;
        }

        public async Task HandleAsync(LoginCommand command) => await _handler
            .RunAsync(async () => await _userservice.LoginAsync(command.Email, command.Password))
            .Next()
            .RunAsync(async () =>
            {
                var user = await _userservice.GetByEmailAsync(command.Email);
                var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
                _memorycache.SetJwt(command.TokentId, jwt);
            })
            .ExecuteAsync();


        //public async Task HandleAsync(Login command)
        //{
        //    await _userservice.LoginAsync(command.Email, command.Password);
        //    var user = await _userservice.GetByEmailAsync(command.Email);
        //    var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
        //    _memorycache.SetJwt(command.TokentId, jwt);
        //}
    }
}
