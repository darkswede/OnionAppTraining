using AutoMapper;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using OnionAppTraining.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVehicleProvider _vehicleProvider;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository, IUserRepository userRepository, IVehicleProvider vehicleProvider, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _vehicleProvider = vehicleProvider;
            _mapper = mapper;
        }

        public async Task<DriverDetailsDTO> GetDriverByIdAsync(Guid userID)
        {
            var driver = await _driverRepository.GetByIdAsync(userID);

            return _mapper.Map<Driver, DriverDetailsDTO>(driver);
        }
        public async Task<IEnumerable<DriverDTO>> GetAllDrivers()
        {
            var drivers = await _driverRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Driver>, IEnumerable<DriverDTO>>(drivers);
        }

        public async Task CreateAsync(Guid userID)
        {
            var user = await _userRepository.GetById(userID);
            if(user == null)
            {
                throw new Exception($"User with id: {userID} not found");
            }
            var driver = await _driverRepository.GetByIdAsync(userID);
            if(driver != null)
            {
                throw new Exception($"Driver with id {userID} alredy exist");
            }

            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);
        }

        public async Task SetVehicleAsync(Guid userId, string brand, string name)
        {
            var driver = await _driverRepository.GetByIdAsync(userId);
            if(driver == null)
            {
                throw new Exception($"driver with id: {userId} was not found");
            }

            var vehicleDetails = await _vehicleProvider.GetAsync(brand, name);
            var vehicle = Vehicle.Create(brand, name, vehicleDetails.Seats);
            driver.SetVehicle(vehicle);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
