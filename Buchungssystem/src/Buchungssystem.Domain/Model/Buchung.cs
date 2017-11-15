using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buchungssystem.Domain.Model
{
    public class Buchung
    {
        [Key]
        public int BuchungId { get; set; }

        [Required]
        public int WarenId { get; set; }

        [ForeignKey("WarenId")]
        public Ware Ware { get; set; }

        [Required]
        public int TischId { get; set; }

        [ForeignKey("TischId")]
        public Tisch Tisch { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public DateTime Zeitpunkt { get; set; }
    }
}
