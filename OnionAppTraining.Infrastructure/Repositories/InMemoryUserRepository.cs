using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnionAppTraining.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>();

        public void Add(User user) => _users.Add(user);

        public IEnumerable<User> GetAll() => _users;

        public User GetByEmail(string email) => _users.Single(x => x.Email == email.ToLowerInvariant());

        public User GetById(Guid id) => _users.Single(x => x.Id == id);

        public void Remove(Guid id)
        {
            var user = GetById(id);
            _users.Remove(user);
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
