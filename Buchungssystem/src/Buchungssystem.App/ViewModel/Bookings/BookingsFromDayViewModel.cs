using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.TableView;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.Bookings
{
    /// <summary>
    /// ViewModel für die Tagesübersichts-View
    /// </summary>
    internal class BookingsFromDayViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemDataPersistence;

        public BookingsFromDayViewModel(IPersistBookingSystemData bookingSystemDataPersistence)
        {
            _bookingSystemDataPersistence = bookingSystemDataPersistence;
            Date = DateTime.Today;
        }

        private ObservableCollection<BookingViewModel> _bookingViewModels;

        /// <summary>
        /// Buchungen an einem Tag
        /// </summary>
        public ObservableCollection<BookingViewModel> BookingViewModels
        {
            get => _bookingViewModels;
            set => SetProperty(ref _bookingViewModels, value, nameof(BookingViewModels));
        }

        /// <summary>
        /// Summe der Beträge aller Bezahlten Buchungen
        /// </summary>
        public decimal Price => BookingViewModels
            .Where(b => b.Booking.Status == BookingStatus.Paid).Sum(b => b.Booking.Price);


        /// <summary>
        /// Datum, von welchem die Buchungen angezeigt werden sollen
        /// </summary>
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                SetProperty(ref _date, value, nameof(value));
                BookingViewModels = new ObservableCollection<BookingViewModel>(GetBookings(_date).Select(b => new BookingViewModel(b)));
            } 
        }

        /// <summary>
        /// Lädt alle Buchungen eines Tages
        /// </summary>
        /// <param name="date"></param>
        private ICollection<Booking> GetBookings(DateTime date)
        {
            var bookings = _bookingSystemDataPersistence.Bookings(date);
            return bookings;
        }
    }
}
