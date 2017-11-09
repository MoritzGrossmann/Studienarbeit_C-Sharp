using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Buchungssystem.App.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T property, T value, [CallerMemberName] string name = "")
        {
            if(!Object.Equals(property, value))
            {
                property = value;
                RaisePropertyChanged(name);
            }
        }
    }
}
