using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IHandler : Iservice
    {
        IHandlerTask RunAsync(Func<Task> run);
        IHandlerTaskRunner Validate(Func<Task> validate);
        Task ExecuteAllAsync();
    }
}
