using System;
using System.Collections.Generic;
using System.Linq;

namespace OnionAppTraining.Core.Domain
{
    public class Driver
    {
        private ISet<Route> _routes = new HashSet<Route>();
        private ISet<DailyRoute> _dailyRoutes = new HashSet<DailyRoute>();

        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public DateTime UpdatedAt { get; private set; }
        public IEnumerable<Route> Routes 
        {
            get { return _routes; }
            set { _routes = new HashSet<Route>(value); }
        }
        public IEnumerable<DailyRoute> DailyRoutes 
        { 
            get { return _dailyRoutes; }
            set { _dailyRoutes = new HashSet<DailyRoute>(value); }
        }

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

        public void AddRoute(string name, Node start, Node end, double distance)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("name cannot be empty");
            }
            var route = _routes.SingleOrDefault(x => x.Name == name);
            if (route == null)
            {
                throw new Exception($"route {name} for driver {Name} alredy exist");
            }

            _routes.Add(Route.Create(name, start, end, distance));
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveRoute(string name)
        {
            var route = _routes.SingleOrDefault(x => x.Name == name);
            if (route == null)
            {
                throw new Exception($"route {name} does not exist");
            }

            _routes.Remove(route);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
