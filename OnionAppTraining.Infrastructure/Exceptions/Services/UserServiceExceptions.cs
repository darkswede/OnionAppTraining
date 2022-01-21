using Passenger.Infrastructure.Exceptions;

namespace OnionAppTraining.Infrastructure.Exceptions.Services
{
    public class UserServiceExceptions : ServiceException
    {
        public UserServiceExceptions(string message) : base(message)
        {
        }

        public new string Code => ErrorCodes.InvalidCredentials;
    }
}
