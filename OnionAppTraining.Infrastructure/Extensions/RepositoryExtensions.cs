using OnionAppTraining.Core.Domain;
using OnionAppTraining.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace OnionAppTraining.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Driver> GetOrFailAsync(this IDriverRepository driverRepository, Guid userId)
        {
            var driver = await driverRepository.GetByIdAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with id '{userId}' do not exist");
            }

            return driver;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, Guid userId)
        {
            var user = await userRepository.GetById(userId);
            if (user == null)
            {
                throw new Exception($"User with id: '{userId}' do not exist");
            }

            return user;
        }
    }
}
