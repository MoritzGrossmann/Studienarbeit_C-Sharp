using System.Collections.Generic;

namespace Buchungssystem.Domain
{
    public class Warengruppe
    {
        public int WarengruppenId { get; set; }
        public string Name { get; set; }
        public virtual List<Ware> Waren { get; set; }
     }
}