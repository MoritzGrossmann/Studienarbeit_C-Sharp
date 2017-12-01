using System;
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

        public string Name { get; set; }

        public ICollection<Table> Tables { get; set; }

        public override void Persist()
        {
            Persistence?.PersistRoom(this);
        }
    }

    public class Table : BookingSystemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public override void Persist()
        {
            Persistence?.PersistTable(this);
        }
    }

    public class ProductGroup : BookingSystemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        public override void Persist()
        {
            Persistence.PersistProductGroup(this);
        }
    }

    public class Product : BookingSystemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public override void Persist()
        {
            Persistence.PersistProduct(this);
        }
    }

    public class Booking : BookingSystemModel
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public Product Product { get; set; }

        public Table Table { get; set; }
        public override void Persist()
        {
            Persistence.Book(this);
        }
    }

    public abstract class BookingSystemModel : INotifyPropertyChanged
    {
        public IPersistBookingSystemData Persistence;

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

        public abstract void Persist();
    }
}
 