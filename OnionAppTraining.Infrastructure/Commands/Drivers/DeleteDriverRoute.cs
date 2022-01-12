using System;

namespace OnionAppTraining.Infrastructure.Commands.Drivers
{
    public class DeleteDriverRoute : ICommand
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
