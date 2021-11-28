using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using OnionAppTraining.Infrastructure.DTO;
using System;

namespace OnionAppTraining.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDTO GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName
            };
        }

        public void Register(string email, string userName, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exist");
            }

            var salt = Guid.NewGuid().ToString("N");
            user = new User(email, password, userName, salt);
            _userRepository.Add(user);
        }
    }
}
