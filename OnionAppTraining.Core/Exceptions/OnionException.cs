using System;

namespace Passenger.Core.Domain
{
    public abstract class OnionException : Exception
    {
        public string Code { get; }

        protected OnionException()
        {
        }

        protected OnionException(string code)
        {
            Code = code;
        }

        protected OnionException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected OnionException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected OnionException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected OnionException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}