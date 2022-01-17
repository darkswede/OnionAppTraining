using System;

namespace OnionAppTraining.Infrastructure.Exceptions
{
    public abstract class ServiceException : Exception
    {
        public abstract string Code { get; }

        public ServiceException(string message) : base(message)
        {
        }
    }
}
