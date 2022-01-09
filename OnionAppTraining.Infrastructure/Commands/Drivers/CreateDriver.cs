using OnionAppTraining.Core.Domain;
using System;

namespace OnionAppTraining.Infrastructure.Commands.Drivers
{
    public class CreateDriver : ICommand
    {
        public Guid UserId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
