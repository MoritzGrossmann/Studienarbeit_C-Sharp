﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Buchungssystem.App
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T property, T value, [CallerMemberName] string name = "")
        {
            if(!Equals(property, value))
            {
                property = value;
                RaisePropertyChanged(name);
            }
        }
    }
}
