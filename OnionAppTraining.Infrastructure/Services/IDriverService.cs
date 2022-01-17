using OnionAppTraining.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IDriverService : Iservice
    {
        Task<DriverDetailsDTO> GetDriverByIdAsync(Guid userId);
        Task<IEnumerable<DriverDTO>> GetAllDrivers();
        Task CreateAsync(Guid userId);
        Task DeleteAsync(Guid userId);
        Task SetVehicleAsync(Guid userId, string brand, string name);
    }
}
