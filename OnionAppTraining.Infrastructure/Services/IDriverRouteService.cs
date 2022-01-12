using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IDriverRouteService
    {
        Task AddAsync(Guid userId, string name, double startLongitude, double startLatitude, double endLongitude, double endLatitude);
        Task RemoveAsync(Guid userId, string name);
    }
}
