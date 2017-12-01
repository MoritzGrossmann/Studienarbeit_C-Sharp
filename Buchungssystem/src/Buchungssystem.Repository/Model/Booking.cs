using System.ComponentModel.DataAnnotations;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Repository.Model
{
    public class DbBooking
    {
        public DbBooking()
        {
        }

        [Key]
        public int DbBookingId { get; set; }

        public int DbProductId { get; set; }
        public DbProduct DbProduct { get; set; }


        public int DbTableId { get; set; }
        public DbTable DbTable { get; set; }

        public int Status { get; set; }

        [Timestamp]
        public byte[] Created { get; set; }
    }
}
