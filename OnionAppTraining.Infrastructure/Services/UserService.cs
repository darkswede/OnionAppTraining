using AutoMapper;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using OnionAppTraining.Infrastructure.DTO;
using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task RegisterAsync(string email, string password, string userName)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exist");
            }

            var salt = Guid.NewGuid().ToString("N");
            user = new User(email, password, userName, salt);
            await _userRepository.AddAsync(user);
        }
    }
}
