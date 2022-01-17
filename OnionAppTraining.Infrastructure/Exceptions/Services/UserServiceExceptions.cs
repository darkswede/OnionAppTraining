namespace OnionAppTraining.Infrastructure.Exceptions.Services
{
    public class UserServiceExceptions : ServiceException
    {
        public UserServiceExceptions(string message) : base(message)
        {
        }

        public override string Code => ErrorCodes.InvalidCredentials;
    }
}
