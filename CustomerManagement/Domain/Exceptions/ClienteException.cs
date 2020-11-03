using System;

namespace CustomerManagement.Domain.Exceptions
{
    public class ClienteException : Exception
    {
        public ClienteException(string message) : base(message)
        {

        }
    }
}