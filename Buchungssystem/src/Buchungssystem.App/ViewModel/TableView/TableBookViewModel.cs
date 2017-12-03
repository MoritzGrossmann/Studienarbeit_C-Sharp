using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
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
            Table = table;
            OpenBookings = new BookingListViewModel(table.Bookings, SelectBooking);
            SelectedBookings = new BookingListViewModel(new List<Booking>(), DeSelectBooking);
            _onReturn = onReturn;
            ToTableListCommand = new RelayCommand(ReturnAction);
        }

        #endregion

        #region Actions

        private void SelectBooking(BookingViewModel bookingViewModel)
        {
            OpenBookings.BookingViewModels.Remove(OpenBookings.BookingViewModels.FirstOrDefault(b => b.Booking.Id == bookingViewModel.Booking.Id));
            bookingViewModel.OnSelect = DeSelectBooking;
            SelectedBookings.BookingViewModels.Add(bookingViewModel);
            RaisePropertyChanged(nameof(OpenBookings));
            RaisePropertyChanged(nameof(SelectedBookings));
        }

        private void DeSelectBooking(BookingViewModel bookingViewModel)
        {
            SelectedBookings.BookingViewModels.Remove(SelectedBookings.BookingViewModels.FirstOrDefault(b => b.Booking.Id == bookingViewModel.Booking.Id));
            bookingViewModel.OnSelect = SelectBooking;
            OpenBookings.BookingViewModels.Add(bookingViewModel);
            RaisePropertyChanged(nameof(OpenBookings));
            RaisePropertyChanged(nameof(SelectedBookings));
        }

        private readonly Action _onReturn;

        #endregion

        #region Commands

        public ICommand ToTableListCommand { get; }

        private void ReturnAction()
        {
            _onReturn.Invoke();
        }

        #endregion
    }
}
