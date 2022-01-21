using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask RunAsync(Func<Task> run);
    }
}
