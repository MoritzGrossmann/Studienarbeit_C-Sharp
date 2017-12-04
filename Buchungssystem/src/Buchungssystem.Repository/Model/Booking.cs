using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Buchungssystem.Domain.Annotations;

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

        [CanBeNull]
        public DateTime? Created { get; set; }

        [CanBeNull]
        public DateTime? Finished { get; set; }
    }
}
