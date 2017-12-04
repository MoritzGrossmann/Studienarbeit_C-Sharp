using System.Collections.Generic;

namespace Buchungssystem.Domain.Model
{
    public class Table : BookingSystemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public Room Room { get; set; }

        public bool Occupied { get; set; }

        public int Places { get; set; }

        public override void Persist()
        {
            Persistence?.PersistTable(this);
        }

        public void Occupy()
        {
            Occupied = true;
            Persistence?.Occupy(this);
        }

        public void Clear()
        {
            Occupied = false;
            Persistence?.Clear(this);
        }
    }
}