using System;

namespace Stundenplan.Domain
{
    public abstract class TimeUnit {

        public DateTime Begin {get; private set;}

        public int Duration {get; private set;}
    }
}