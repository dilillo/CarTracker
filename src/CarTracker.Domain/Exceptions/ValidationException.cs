using System;

namespace CarTracker.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) 
        {
        }
    }
}
