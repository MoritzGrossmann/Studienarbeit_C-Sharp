using System;
using System.ComponentModel.DataAnnotations;
using Buchungssystem.Domain.Properties;

namespace Buchungssystem.Repository.Model
{
    public class DbBooking
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public DbProduct Product { get; set; }


        public int TableId { get; set; }
        public DbTable Table { get; set; }

        public int Status { get; set; }

        public decimal Price { get; set; }

        [CanBeNull]
        public DateTime? Created { get; set; }

        [CanBeNull]
        public DateTime? Finished { get; set; }
    }
}
