using Microsoft.Extensions.Caching.Memory;
using OnionAppTraining.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public class VehicleProvider : IVehicleProvider
    {
        private IMemoryCache _memoryCache;
        private static readonly string CacheKey = "Vehicles";
        private static readonly IDictionary<string, IEnumerable<VehicleDetails>> availableVehicles = new Dictionary<string, IEnumerable<VehicleDetails>>
        {
            ["Audi"] = new List<VehicleDetails>
            {
                new VehicleDetails("RS8", 5)
            },
            ["BMW"] = new List<VehicleDetails>
            {
                new VehicleDetails("i8", 3),
                new VehicleDetails("E36", 5)
            },
            ["Ford"] = new List<VehicleDetails>
            {
                new VehicleDetails("FIesta", 5)
            },
            ["Skoda"] = new List<VehicleDetails>
            {
                new VehicleDetails("Fabia", 5),
                new VehicleDetails("Rapid", 5)
            },
            ["Volkswagen"] = new List<VehicleDetails>
            {
                new VehicleDetails("Passat", 5)
            }
        };

        public VehicleProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<VehicleDTO>> GetAllAsync()
        {
            var vehicles = _memoryCache.Get<IEnumerable<VehicleDTO>>(CacheKey);
            if (vehicles == null)
            {
                Console.WriteLine("getting from database");
                vehicles = await GetVehiclesAsync();
                _memoryCache.Set(CacheKey, vehicles);
            }
            else 
            {
                Console.WriteLine("getting from cache");
            }
            return vehicles;
        }

        public async Task<VehicleDTO> GetAsync(string brand, string name)
        {
            if (!availableVehicles.ContainsKey(brand))
            {
                throw new Exception($"Vehicle brand: {brand} not available.");
            }
            var vehicles = availableVehicles[brand];
            var vehicle = vehicles.SingleOrDefault(x => x.Name == name);
            if (vehicle == null)
            {
                throw new Exception($"Vehicle: {name} for brand: {brand} is not available");
            }

            return await Task.FromResult(new VehicleDTO 
            { 
                Brand = brand,
                Name = vehicle.Name,
                Seats = vehicle.Seats
            });
        }

        private async Task<IEnumerable<VehicleDTO>> GetVehiclesAsync() =>
            await Task.FromResult(availableVehicles.GroupBy(x => x.Key)
                .SelectMany(g => g.SelectMany(v => v.Value.Select(c => new VehicleDTO
                {
                    Brand = v.Key,
                    Name = c.Name,
                    Seats = c.Seats
                }))));

        private class VehicleDetails
        {
            public string Name { get; }
            public int Seats { get; }

            public VehicleDetails(string name, int seats)
            {
                Name = name;
                Seats = seats;
            }
        }
    }
}
