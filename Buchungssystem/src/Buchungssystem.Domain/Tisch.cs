using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buchungssystem.Domain
{
    public class Tisch
    {
        [Key]
        public int TischId { get; set; }

        [Required]
        public int Nummer { get; set; }
        public int Sitzplaetze { get; set; }

        [ForeignKey("RaumId")]
        public int RaumId { get; set; }
        public virtual Raum Raum { get; set; }
    }
}