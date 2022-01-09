using OnionAppTraining.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IUserService : Iservice
    {
        Task<UserDTO> GetByEmailAsync(string email);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task RegisterAsync(Guid guid, string email, string password,  string role, string username);
        Task LoginAsync(string email, string password);
    }
}
