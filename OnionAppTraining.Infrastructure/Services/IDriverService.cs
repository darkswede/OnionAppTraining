using OnionAppTraining.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IDriverService : Iservice
    {
        Task<DriverDTO> GetDriverByIdAsync(Guid userID);
        Task<IEnumerable<DriverDTO>> GetAllDrivers();
        Task CreateAsync(Guid userID);
        Task SetVehicleAsync(Guid userId, string brand, string name);
    }
}
