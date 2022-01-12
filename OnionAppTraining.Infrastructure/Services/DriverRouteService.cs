using AutoMapper;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(Guid userId, string name, double startLongitude, double startLatitude, double endLongitude, double endLatitude)
        {
            var driver = await _driverRepository.GetByIdAsync(userId);
            if (driver == null)
            {
                throw new Exception("driver with given ID do not exist");
            }
            var start = Node.Create("Start Address", startLongitude, endLatitude);
            var end = Node.Create("End Address", endLongitude, endLatitude);

            driver.AddRoute(name, start, end);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task RemoveAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetByIdAsync(userId);
            if (driver == null)
            {
                throw new Exception("driver with given ID do not exist");
            }

            driver.RemoveRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
