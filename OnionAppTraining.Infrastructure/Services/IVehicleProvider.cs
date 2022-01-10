using OnionAppTraining.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IVehicleProvider : Iservice
    {
        Task<IEnumerable<VehicleDTO>> GetAllAsync();
        Task<VehicleDTO> GetAsync(string brand, string name);
    }
}
