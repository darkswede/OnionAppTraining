using OnionAppTraining.Core.Domain;
using System;
using System.Collections.Generic;

namespace OnionAppTraining.Core.Repositories
{
    public interface IUserRepository
    {
        public User GetById(Guid id);
        public User GetByEmail(string email);
        public IEnumerable<User> GetAll();
        public void Add(User user);
        public void Update(User user);
        public void Remove(Guid id);
    }
}
