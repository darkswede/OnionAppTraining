﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository, IUserRepository userRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<DriverDTO> GetDriverByIdAsync(Guid userID)
        {
            var driver = await _driverRepository.GetByIdAsync(userID);

            return _mapper.Map<Driver, DriverDTO>(driver);
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

        public async Task SetVehicleAsync(Guid userId, string brand, string name, int seats)
        {
            var driver = await _driverRepository.GetByIdAsync(userId);
            if(driver == null)
            {
                throw new Exception($"driver with id: {userId} was not found");
            }

            driver.SetVehicle(brand, name, seats);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}