using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Repository.Model
{
    public class DbTable
    {
        public DbTable()
        {
            
        }

        [Key]
        public int DbTableId { get; set; }

        public int Places { get; set; }

        public string Name { get; set; }

        public int DbRoomId { get; set; }

        public DbRoom DbRoom { get; set; }

        public ICollection<DbBooking> DbBookings { get; set; }

        public bool Occupied { get; set; }
    }
}
