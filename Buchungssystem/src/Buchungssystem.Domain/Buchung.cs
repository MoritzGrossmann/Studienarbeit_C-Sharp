using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buchungssystem.Domain
{
    public class Buchung
    {
        [Key]
        public int BuchungsId { get; set; }

        [Required]
        [ForeignKey("TischId")]
        public int TischId { get; set; }
        public virtual Tisch Tisch { get; set; }

        [Required]
        [ForeignKey("WarenId")]
        public int WarenId { get; set; }
        public virtual Ware Ware { get; set; }
        public DateTime BuchungsZeitpunkt { get; set; }


        public int BuchungsStatus { get; set; }
    }
}