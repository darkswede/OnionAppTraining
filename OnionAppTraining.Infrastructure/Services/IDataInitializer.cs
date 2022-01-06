using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IDataInitializer : Iservice
    {
        Task SeedAsync();
    }
}
