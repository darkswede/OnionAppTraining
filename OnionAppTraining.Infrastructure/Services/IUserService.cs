using OnionAppTraining.Infrastructure.DTO;

namespace OnionAppTraining.Infrastructure.Services
{
    public interface IUserService
    {
        public UserDTO GetByEmail(string email);
        public void Register(string email, string userName, string password);
    }
}
