using Passenger.Core.Domain;

namespace OnionAppTraining.Core.Exceptions.UserExceptions
{
    public class InvalidUsernameException : DomainException
    {
        public InvalidUsernameException(string message) : base(message)
        {
        }

        public new string Code => ErrorCodes.InvalidUsername;
    }
}
