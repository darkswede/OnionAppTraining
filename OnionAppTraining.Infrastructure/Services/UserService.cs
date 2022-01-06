using AutoMapper;
using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using OnionAppTraining.Infrastructure.DTO;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
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

            return _mapper.Map<User, UserDTO>(user);
        }

        public async Task RegisterAsync(Guid guid, string email, string password, string role, string username)
        {
            var user = await _userRepository.GetByEmailAsync(ValidateEmail(email));
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exist");
            }

            ValidatePassword(password);
            await ValidateUsername(username);

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);
            user = new User(guid, email, hash, role, username, salt);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid Credentials");
            }

            var hash = _encrypter.GetHash(password, user.Salt);
            if (user.Password == hash)
            {
                return;
            }

            throw new Exception("Invalid Credentials");
        }

        private static string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"Email: {email} not exist");
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

            var passwordRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            if (!passwordRegex.IsMatch(password))
            {
                throw new Exception($"Password do not match the rules");
            }

            return password;
        }

        private async Task ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception($"Field 'username' cannot be empty");
            }

            var userNameRegex = new Regex(@"^.{6,}$");
            if (!userNameRegex.IsMatch(username))
            {
                throw new Exception($"Username do not match the rules");
            }

            var user = await _userRepository.GetByUsernameAsync(username);
            if (user != null)
            {
                throw new Exception($"Username: {user.UserName} is in use");
            }
        }
    }
}
