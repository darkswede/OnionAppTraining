using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public class RouteManager : IRouteManager
    {
        private static readonly Random Random = new Random();

        public async Task<string> GetAddressAsync(double longitude, double latitude) => await Task.FromResult($"sample address {Random.Next(100)}");

        public double CalculateDistance(double startLongitude, double startLatitude, double endLongitude, double endLatitude) => Random.Next(500, 10000);
    }
}
