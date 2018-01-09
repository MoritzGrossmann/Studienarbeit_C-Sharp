using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Repository.Model
{
    public class DbTable
    {
        [Key]
        public int Id { get; set; }

        public int Places { get; set; }

        public string Name { get; set; }

        public int RoomId { get; set; }

        public DbRoom Room { get; set; }

        public ICollection<DbBooking> Bookings { get; set; }

        public bool Occupied { get; set; }

        public bool Deleted { get; set; }
    }
}
