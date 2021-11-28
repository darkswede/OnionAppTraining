using System;

namespace OnionAppTraining.Core.Domain
{
    public class DailyRoute
    {
        public Guid Id { get; protected set; }
        public Route Route { get; protected set; }
        public IEquatable<PassengerNode> PassengerNodes { get; protected set; }
    }
}
