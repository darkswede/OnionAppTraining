using System;

namespace OnionAppTraining.Infrastructure.Commands
{
    public class AuthenticatedCommandBase : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }
    }
}
