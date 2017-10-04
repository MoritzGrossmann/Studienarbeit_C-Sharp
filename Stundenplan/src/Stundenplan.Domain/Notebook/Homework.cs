using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan.Domain.Notebook
{
    public class Homework
    {
        public Lesson Lesson { get; private set; }
        
        public String Subject { get; private set; }
    }
}
