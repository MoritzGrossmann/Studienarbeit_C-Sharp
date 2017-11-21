using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Domain.Model
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public List<Table> Tables { get; set; }
    }
}
