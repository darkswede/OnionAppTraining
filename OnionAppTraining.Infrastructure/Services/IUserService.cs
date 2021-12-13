using OnionAppTraining.Infrastructure.DTO;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IUserService : Iservice
    {
        public Task<UserDTO> GetByEmailAsync(string email);
        public Task RegisterAsync(string email, string userName, string password);
    }
}
