using System;

namespace OnionAppTraining.Core.Domain
{
    public class Driver
    {
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public DateTime UpdatedAt { get; private set; }
        public IEquatable<Route> Routes { get; protected set; }
        public IEquatable<DailyRoute> DailyRoutes { get; protected set; }

        public Driver(User user)
        {
            UserId = user.Id;
            Name = user.UserName;
        }

        public void SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
