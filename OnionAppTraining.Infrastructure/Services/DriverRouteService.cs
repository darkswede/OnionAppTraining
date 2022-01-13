using AutoMapper;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using OnionAppTraining.Infrastructure.Extensions;
using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IRouteManager _routeManager;
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository, IRouteManager routeManager, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _routeManager = routeManager;
            _mapper = mapper;
        }

        public async Task AddAsync(Guid userId, string name, double startLongitude, double startLatitude, double endLongitude, double endLatitude)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            var startAddress = await _routeManager.GetAddressAsync(startLongitude, startLatitude);
            var endAddress = await _routeManager.GetAddressAsync(endLongitude, endLatitude);
            var startNode = Node.Create(startAddress, startLongitude, endLatitude);
            var endNode = Node.Create(endAddress, endLongitude, endLatitude);
            var distance = _routeManager.CalculateDistance(startLongitude, startLatitude, endLongitude, endLatitude);

            driver.AddRoute(name, startNode, endNode, distance);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task RemoveAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);

            driver.RemoveRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
