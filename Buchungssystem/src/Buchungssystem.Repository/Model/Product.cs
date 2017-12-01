using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Repository.Model
{
    public class DbProduct
    {
        public DbProduct()
        {
            
        }

        [Key]
        public int DbProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int DbProductGroupId { get; set; }
      
        public DbProductGroup DbProductGroup { get; set; }

        public ICollection<DbBooking> DbBookings { get; set; }

        public bool Deleted { get; set; }
    }
}
