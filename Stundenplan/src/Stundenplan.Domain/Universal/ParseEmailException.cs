using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan.Domain.Universal
{
    public class ParseEmailException : Exception
    {
        public ParseEmailException()
        {
            
        }

        public ParseEmailException(String message) : base(message)
        {
            
        }

        public ParseEmailException(String message, Exception innerException) : base(message, innerException)
        {
            
        }

        public static readonly string NO_AT = "Email enthät kein @-zeichen";
    }

    
}
