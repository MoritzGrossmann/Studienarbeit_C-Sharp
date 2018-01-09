using System;

namespace Buchungssystem.Domain.Database
{
    public class ModelExistException : Exception
    {
        public ModelExistException()
        {
            
        }
        public ModelExistException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public ModelExistException(string message) : base(message)
        {
            
        }
    }
}
