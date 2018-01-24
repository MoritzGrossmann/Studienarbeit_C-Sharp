using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Repository.Model
{
    /// <summary>
    /// Datenbankrepräsentation einer Ware
    /// </summary>
    public class DbProduct
    {
        // ReSharper disable once UnassignedGetOnlyAutoProperty : Id wird von Datenbank inititalisiert

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
