using Autofac;
using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        public readonly IComponentContext _componentContext;

        public CommandDispatcher(IComponentContext context)
        {
            _componentContext = context;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), $"Command '{typeof(T).Name}' can't be null");
            }

            var handler = _componentContext.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}
