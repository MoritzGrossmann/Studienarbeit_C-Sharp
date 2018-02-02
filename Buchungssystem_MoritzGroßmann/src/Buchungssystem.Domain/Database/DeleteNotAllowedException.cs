using System;

namespace Buchungssystem.Domain.Database
{
    public class DeleteNotAllowedException : Exception
    {
        public DeleteNotAllowedException()
        {

        }
        public DeleteNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public DeleteNotAllowedException(string message) : base(message)
        {

        }
    }
}
