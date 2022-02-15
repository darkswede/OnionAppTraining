using AutoMapper;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using OnionAppTraining.Infrastructure.DTO;
using OnionAppTraining.Infrastructure.Exceptions.Services;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IMapper _mapper;

       
        public UserService(IUserRepository userRepository, IEncrypter encrypter, IMapper mapper)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var validatedEmail = ValidateEmail(email);

            var user = await _userRepository.GetByEmailAsync(validatedEmail);

            return _mapper.Map<UserDTO>(user);
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task RegisterAsync(Guid guid, string email, string password, string role, string username)
        {
            ValidatePassword(password);
            await ValidateUsername(username);

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);
            var user = new User(guid, email, hash, role, username, salt);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new UserServiceExceptions("Invalid Credentials");
            }

            var hash = _encrypter.GetHash(password, user.Salt);
            if (user.Password == hash)
            {
                return;
            }

            throw new UserServiceExceptions("Invalid Credentials");
        }

        private static string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"Email cannot be empty");
            }

            var trimmed = email.Trim();
            string validatedEmail;
            try
            {
                validatedEmail = new MailAddress(trimmed).Address;
            }
            catch(FormatException)
            {
                throw new Exception("Email is not correct");
            }

            return validatedEmail;
        }

        private static string ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"Field 'password' cannot be empty");
            }

            return password;
        }

        private async Task ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception($"Field 'username' cannot be empty");
            }

            var user = await _userRepository.GetByUsernameAsync(username);
            if (user != null)
            {
                throw new Exception($"Username: {user.UserName} is in use");
            }
        }
    }
}
