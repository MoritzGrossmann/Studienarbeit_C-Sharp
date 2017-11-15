using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Domain.Model
{
    public class Warengruppe
    {
        [Key]
        public int WarengruppenId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Ware> Waren { get; set; }
    }
}
