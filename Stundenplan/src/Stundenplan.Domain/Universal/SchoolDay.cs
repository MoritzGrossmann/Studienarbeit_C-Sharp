using System;
using System.Collections.Generic;

namespace Stundenplan.Domain
{
    public class SchoolDay {

        public DateTime Day {get; private set;}

        public List<TimeUnit> Units {get; private set;}

        public int DayOfWeek {get; private set;}
    }
}