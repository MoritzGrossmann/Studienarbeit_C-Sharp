using System.ComponentModel;
using System.Runtime.CompilerServices;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.Domain.Model
{
    /// <summary>
    /// Abstrakte Oberklasse für alle Models in diesem System
    /// </summary>
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