using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.User;
using OnionAppTraining.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Handlers.User
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Password, command.Role, command.Username);
        }
    }
}
