using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buchungssystem.Domain.Model
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int TableId { get; set; }

        [ForeignKey("TableId")]
        public Table Table { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
