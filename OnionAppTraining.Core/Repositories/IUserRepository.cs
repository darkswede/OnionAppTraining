using OnionAppTraining.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionAppTraining.Core.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetById(Guid id);
        public Task<User> GetByEmailAsync(string email);
        public Task<User> GetByUsernameAsync(string username);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task AddAsync(User user);
        public Task UpdateAsync(User user);
        public Task RemoveAsync(Guid id);
    }
}
