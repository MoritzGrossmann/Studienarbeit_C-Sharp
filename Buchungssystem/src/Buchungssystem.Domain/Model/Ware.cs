using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buchungssystem.Domain.Model
{
    public class Ware
    {
        [Key]
        public int WarenId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Preis { get; set; }

        [Required]
        public int WarengruppenId { get; set; }

        [ForeignKey("WarengruppenId")]
        public Warengruppe Warengruppe { get; set; }

        public List<Buchung> Buchungen { get; set; }

        [Required]
        public bool Deleted { get; set; }
    }
}
