using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buchungssystem.Domain.Model
{
    public class Tisch
    {
        [Key]
        public int TischId { get; set; }

        public int Plaetze { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public int RaumId { get; set; }

        [ForeignKey("RaumId")]
        public Raum Raum { get; set; }

        public List<Buchung> Buchungen { get; set; }
    }
}
