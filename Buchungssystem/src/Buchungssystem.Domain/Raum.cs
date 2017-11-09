using System.Collections.Generic;

namespace Buchungssystem.Domain
{
    public class Raum
    {
        public int RaumId { get; set; }

        public string Name { get; set; }

        public virtual List<Tisch> Tische { get; set; }
    }
}