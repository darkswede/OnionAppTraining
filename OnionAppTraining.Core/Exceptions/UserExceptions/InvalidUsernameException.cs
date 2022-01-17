namespace OnionAppTraining.Core.Exceptions.UserExceptions
{
    public class InvalidUsernameException : DomainException
    {
        public InvalidUsernameException(string message) : base(message)
        {
        }

        public override string Code => ErrorCodes.InvalidUsername;
    }
}
