using System.Collections.Generic;

namespace Buchungssystem.Domain.Model
{
    public class Room : BookingSystemModel
    {
        public int Id { get; set; }


        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; RaisePropertyChanged(nameof(Name)); }
        }

        public ICollection<Table> Tables { get; set; }

        public Room Persist()
        {
            return Persistence?.PersistRoom(this);
        }

        public void Delete()
        {
            Persistence.DeleteRoom(this);
        }
    }
}
 