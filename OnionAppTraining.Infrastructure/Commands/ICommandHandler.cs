using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
