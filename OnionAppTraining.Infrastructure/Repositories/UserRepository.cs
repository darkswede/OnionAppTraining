using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository, IMongoRepository
    {
        private IMongoCollection<User> Users => _mongoDatabase.GetCollection<User>("Users");
        private readonly IMongoDatabase _mongoDatabase;

        public UserRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<User> GetById(Guid id) => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetByEmailAsync(string email) => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User> GetByUsernameAsync(string username) => await Users.AsQueryable().FirstOrDefaultAsync(x => x.UserName == username);

        public async Task<IEnumerable<User>> GetAllAsync() => await Users.AsQueryable().ToListAsync();

        public async Task AddAsync(User user) => await Users.InsertOneAsync(user);

        public async Task UpdateAsync(User user) => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);

        public async Task RemoveAsync(Guid id) => await Users.DeleteOneAsync(x => x.Id == id);
    }
}
