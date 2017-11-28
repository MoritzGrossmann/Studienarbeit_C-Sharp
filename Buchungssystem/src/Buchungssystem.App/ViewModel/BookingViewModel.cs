using System;
using System.Globalization;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    internal class BookingViewModel : BaseViewModel
    {
        private readonly IPersistBaseData _baseDataPersistence;

        private readonly IPersistBooking _bookingPersistence;

        private readonly Booking _booking;
        public Booking Booking => _booking;

        private readonly Product _product;

        public string Ware => _product.Name;

        public decimal Price => _product.Price;

        public string TimeStamp => _booking.Timestamp.ToShortTimeString();

        public BookingViewModel(Booking booking)
        {
            _booking = booking;
        }

        public BookingViewModel(IPersistBaseData baseDataPersistence, IPersistBooking bookingPersistence, Booking booking, Action<Booking> onSelect)
        {
            _baseDataPersistence = baseDataPersistence;
            _bookingPersistence = bookingPersistence;
            _onSelect = onSelect;
            _booking = booking;
            _product = _bookingPersistence.Product(_booking);

            SelectCommand = new RelayCommand(Select);
        }

        #region Commands

        private readonly Action<Booking> _onSelect;

        public BookingViewModel(IPersistBaseData baseDataPersistence, IPersistBooking bookingPersistence, Booking booking)
        {
            _baseDataPersistence = baseDataPersistence;
            _bookingPersistence = bookingPersistence;
            _booking = booking;
            _product = _bookingPersistence.Product(_booking);
        }

        public ICommand SelectCommand { get; }

        private void Select()
        {
            _onSelect?.Invoke(_booking);
        }

        #endregion
    }
}