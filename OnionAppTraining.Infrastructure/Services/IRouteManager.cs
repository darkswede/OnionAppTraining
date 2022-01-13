using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IRouteManager : Iservice
    {
        Task<string> GetAddressAsync(double longitude, double latitude);
        double CalculateDistance(double startLongitude, double startLatitude, double endLongitude, double endLatitude);
    }
}
