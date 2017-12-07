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
