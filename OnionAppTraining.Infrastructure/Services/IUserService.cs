using OnionAppTraining.Infrastructure.DTO;
using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IUserService : Iservice
    {
        public Task<UserDTO> GetByEmailAsync(string email);
        public Task RegisterAsync(Guid guid, string email, string password,  string role, string username);
        public Task LoginAsync(string email, string password);
    }
}
