using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stundenplan.Domain.Universal;

namespace Stundenplan.Domain
{
    public class Teacher
    {
        public Sex Sex { get; private set; }

        public String Name { get; private set; }

        public Email Email { get; set; }
    }
}
