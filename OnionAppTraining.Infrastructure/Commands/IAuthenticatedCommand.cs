using System;

namespace OnionAppTraining.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid UserId { get; set; }
    }
}
