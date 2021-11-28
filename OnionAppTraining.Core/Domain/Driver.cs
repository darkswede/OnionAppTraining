using System;

namespace OnionAppTraining.Core.Domain
{
    public class Driver
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public IEquatable<Route> Routes { get; protected set; }
        public IEquatable<DailyRoute> DailyRoutes { get; protected set; }
    }
}
