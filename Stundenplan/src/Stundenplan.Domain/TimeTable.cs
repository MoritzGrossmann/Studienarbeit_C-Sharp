using System;
using System.Collections.Generic;

namespace Stundenplan.Domain
{
    public class TimeTable {
        
        public String Name { get; set; }

        public IEnumerable<Discipline> Disciplines { get; set; }
    }
}