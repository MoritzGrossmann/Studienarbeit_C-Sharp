using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Domain
{
    public class Raum
    {
        [Key]
        public int RaumId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<Tisch> Tische { get; set; }
    }
}