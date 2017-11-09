using System;

namespace Buchungssystem.Domain
{
    public class Reservierung
    {
        public int ReservierungsId { get; set; }

        public string Name { get; set; }

        public DateTime Uhrzeit { get; set; }

        public int TischId { get; set; }

        public virtual Tisch Tisch { get; set; }
    }
}