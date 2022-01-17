using AutoMapper;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using OnionAppTraining.Infrastructure.DTO;
using OnionAppTraining.Infrastructure.Extensions;
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

            return _mapper.Map<DriverDetailsDTO>(driver);
        }
        public async Task<IEnumerable<DriverDTO>> GetAllDrivers()
        {
            var drivers = await _driverRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DriverDTO>>(drivers);
        }

        public async Task CreateAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var driver = await _driverRepository.GetByIdAsync(userId);
            if(driver != null)
            {
                throw new Exception($"Driver with id {userId} alredy exist");
            }

            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);
        }

        public async Task DeleteAsync(Guid userId)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            await _driverRepository.DeleteAsync(driver);
        }

        public async Task SetVehicleAsync(Guid userId, string brand, string name)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);

            var vehicleDetails = await _vehicleProvider.GetAsync(brand, name);
            var vehicle = Vehicle.Create(brand, name, vehicleDetails.Seats);
            driver.SetVehicle(vehicle);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
