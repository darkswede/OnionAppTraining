using System;

namespace OnionAppTraining.Infrastructure.Commands.User
{
    public class LoginCommand : ICommand
    {
        public Guid TokentId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
