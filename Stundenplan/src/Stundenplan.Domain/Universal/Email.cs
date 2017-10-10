using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan.Domain.Universal
{
    public class Email
    {
        public String Value { get; private set; }

        public String Domain { get; private set; }

        public String UserName { get; private set; }

        public Email(String email)
        {
            String[] parts = email.Split('@');
            if (parts.Length == 1) { throw new ParseEmailException(ParseEmailException.NO_AT);}
            this.Domain = parts[parts.Length-1];
            this.UserName = getUserName(email);
        }

        private bool containsForbiddenChars()
        {
            return false;
        }

        private String getUserName(String email)
        {
            string username = "";
            String[] splitted = email.Split('@');
            for (int i = 0; i < splitted.Length - 1; i++)
            {
                username+=splitted[i];
            }
            return username;
        }

        private static HashSet<Char> _allowedChars = new HashSet<char>() {'.', '!','#','$','%','&','*','+','-','/','=','?','^','_','`','{','|','}','~'};

        private static HashSet<Char> _specialAllowdChars =
            new HashSet<char>() {'(', ')', ',', ':', ';', '<', '>', '@', '[', '\\', ']'};
    }
}
