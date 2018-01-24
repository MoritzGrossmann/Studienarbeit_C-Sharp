using System;

namespace Buchungssystem.Domain.Database
{
    public class ModelNotExistException : Exception
    {
        public ModelNotExistException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public ModelNotExistException(string message) : base(message)
        {
            
        }
    }
}