using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Domain
{
    public class Warengruppe
    {
        [Key]
        public int WarengruppenId { get; set; }
        public string Name { get; set; }
        public virtual List<Ware> Waren { get; set; }
     }
}