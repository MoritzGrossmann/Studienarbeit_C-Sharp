using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Annotations;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.Domain.Model
{
    public class Room : BookingSystemModel
    {
        public Room()
        {
        }

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
    }

    public abstract class BookingSystemModel : INotifyPropertyChanged
    {
        public IPersistBookingSystemData Persistence { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T property, T value, [CallerMemberName] string name = "")
        {
            if (!Equals(property, value))
            {
                property = value;
                RaisePropertyChanged(name);
            }
        }
    }
}
 