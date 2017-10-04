using System;
using System.Collections.Generic;

namespace Stundenplan.Domain
{
    public class Timetable {

        public List<SchoolDay> Days {get; private set;}

        public DateTime Expires {get; private set;}
    }
}