using OnionAppTraining.Infrastructure.Commands;
using OnionAppTraining.Infrastructure.Commands.User;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Handlers.User
{
    public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassowrdCommand>
    {
        public async Task HandleAsync(ChangeUserPassowrdCommand command)
        {
            await Task.CompletedTask;
        }
    }
}
