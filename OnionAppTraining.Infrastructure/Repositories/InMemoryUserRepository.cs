using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>();

        public async Task<User> GetById(Guid id) => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetByEmailAsync(string email) => await Task.FromResult(_users.FirstOrDefault(x => x.Email == email.ToLowerInvariant()));

        public async Task<User> GetByUsernameAsync(string username) => await Task.FromResult(_users.FirstOrDefault(x => x.UserName == username));

        public async Task<IEnumerable<User>> GetAllAsync() => await Task.FromResult(_users);

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetById(id);
            _users.Remove(user);
            await Task.CompletedTask;
        }
    }
}
