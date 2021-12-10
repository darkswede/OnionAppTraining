using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.User;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Handlers.User
{
    public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassowrd>
    {
        public async Task HandleAsync(ChangeUserPassowrd command)
        {
            await Task.CompletedTask;
        }
    }
}
