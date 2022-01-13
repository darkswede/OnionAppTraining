using System;

namespace OnionAppTraining.Core.Domain
{
    public class Route
    {
        public string Name { get; protected set; }
        public Node Start { get; protected set; }
        public Node End{ get; protected set; }
        public double Distance { get; protected set; }
        public DateTime UpdatedAt { get; set; }

        protected Route(string name, Node start, Node end, double distance)
        {
            Name = name;
            Start = start;
            End = end;
            Distance = distance;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name cannot be empty");
            }

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Route Create(string name, Node start, Node end, double distance) => new Route(name, start, end, distance);
    }
}
