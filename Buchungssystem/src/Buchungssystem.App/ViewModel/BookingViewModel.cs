using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    internal class BookingViewModel : BaseViewModel
    {

        private Booking _booking;
        public Booking Booking => _booking;

        public string Ware => Booking.Product.Name;

        public decimal Preis => Booking.Product.Price;

        public BookingViewModel(Booking booking)
        {
            _booking = booking;
        }

        public BookingViewModel(Booking booking, Action<Booking> onSelect)
        {
            _onSelect = onSelect;
            _booking = booking;

            SelectCommand = new RelayCommand(Select);
        }

        #region Commands

        private readonly Action<Booking> _onSelect;

        public ICommand SelectCommand { get; }

        public void Select()
        {
            _onSelect?.Invoke(_booking);
        }

        #endregion
    }
}