using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
