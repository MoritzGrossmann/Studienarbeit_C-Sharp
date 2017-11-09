using System;

namespace Buchungssystem.Domain
{
    public class Ware
    {
        public int WarenId { get; set; }

        public string Name { get; set; }

        public Decimal Preis { get; set; }

        public int WarengruppenId { get; set; }

        public virtual Warengruppe Warengruppe { get; set; }
    }
}