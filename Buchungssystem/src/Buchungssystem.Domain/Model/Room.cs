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


        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; RaisePropertyChanged(nameof(Name)); }
        }

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

        public ProductGroup ProductGroup { get; set; }
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

        public BookingStatus Status { get; set; }
        public override void Persist()
        {
            Persistence.Book(this);
        }

        public void Pay()
        {
            Persistence.Pay(this);
            Status = BookingStatus.Paid;
        }

        public void Cancel()
        {
            Persistence.Cancel(this);
            Status = BookingStatus.Cancled;
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
 