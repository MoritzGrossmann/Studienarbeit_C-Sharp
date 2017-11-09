using System;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Domain
{
    public class Reservierung
    {
        [Key]
        public int ReservierungsId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Uhrzeit { get; set; }

        public int TischId { get; set; }

        public virtual Tisch Tisch { get; set; }
    }
}