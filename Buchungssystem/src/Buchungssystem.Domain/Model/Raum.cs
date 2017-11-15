using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Domain.Model
{
    public class Raum
    {
        [Key]
        public int RaumId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<Tisch> Tische { get; set; }
    }
}
