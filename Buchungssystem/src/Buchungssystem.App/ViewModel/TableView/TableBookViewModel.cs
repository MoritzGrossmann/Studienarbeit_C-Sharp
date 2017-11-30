using System;
using System.Collections.Generic;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.TableView
{

    internal class TableBookViewModel : BaseViewModel
    {
        #region Properties

        private Table _table;

        public Table Table
        {
            get => _table;
            set => _table = value;
        }

        private BookingListViewModel _openBookings;

        public BookingListViewModel OpenBookings
        {
            get => _openBookings;
            set => _openBookings = value;
        }

        private BookingListViewModel _selectedBookings;

        public BookingListViewModel SelectedBookings
        {
            get => _selectedBookings;
            set => _selectedBookings = value;
        }

        #endregion

        #region Contructor

        public TableBookViewModel(Table table, Action onReturn)
        {
            _table = table;
            _openBookings = new BookingListViewModel(table.Bookings, SelectBooking);
            _selectedBookings = new BookingListViewModel(new List<Booking>(), SelectBooking);
        }

        #endregion

        #region Actions

        private void SelectBooking(BookingViewModel bookingViewModel)
        {
            SelectedBookings.AddBookingViewModel(bookingViewModel, DeSelectBooking);
        }

        private void DeSelectBooking(BookingViewModel bookingViewModel)
        {
            OpenBookings.AddBookingViewModel(bookingViewModel, SelectBooking);
        }

        #endregion
    }
}
