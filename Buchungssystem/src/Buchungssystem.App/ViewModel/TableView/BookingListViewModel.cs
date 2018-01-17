using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel.TableView
{
    /// <summary>
    /// Repräsentiert eine Liste von BookingViewModel
    /// </summary>
    internal class BookingListViewModel : BaseViewModel
    {
        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        /// <param name="bookings">Buchungen, welche im BookingListViewModel angezeigt werden soll</param>
        /// <param name="onBookingSelect">Methode, die aufgerufen wird, wenn eine Buchung ausgewählt wurde</param>
        public BookingListViewModel(ICollection<Booking> bookings, Action<BookingViewModel> onBookingSelect)
        {
            _bookingViewModels = new ObservableCollection<BookingViewModel>(bookings.Select(b => new BookingViewModel(b, SelectBooking)));
            _selectBooking = onBookingSelect;
        }

        #region Properties

        private ObservableCollection<BookingViewModel> _bookingViewModels;

        /// <summary>
        /// Repräsentiert alle BookingViewModel
        /// </summary>
        public ObservableCollection<BookingViewModel> BookingViewModels
        {
            get => _bookingViewModels;
            set => SetProperty(ref _bookingViewModels, value, nameof(BookingViewModels));
        }

        /// <summary>
        /// Summe der Preise aller Buchungen
        /// </summary>
        public decimal Price
        {
            get
            {
                decimal sum = 0;
                _bookingViewModels.ForEach(bvm => sum += bvm.Booking.Price);
                return sum;
            }
        }

        #endregion

        #region Actions

        private readonly Action<BookingViewModel> _selectBooking;

        /// <summary>
        /// Ruft die im Konstruktor übergebene Methode onBookingSelect auf und übergibt das BookingViewModel
        /// </summary>
        /// <param name="bookingViewModel">BookingViewModel, welches ausgewählt wurde</param>
        private void SelectBooking(BookingViewModel bookingViewModel)
        {
            _selectBooking?.Invoke(bookingViewModel);
            RaisePropertyChanged(nameof(BookingViewModels));
            RaisePropertyChanged(nameof(Price));
        }

        /// <summary>
        /// Fügt der Liste BookingViewModels das übergebene BookingViewModel hinzu
        /// </summary>
        /// <param name="bookingViewModel">BookingViewModel, welches der List BookingViewModels hinzugefügt werden soll</param>
        public void Add(BookingViewModel bookingViewModel)
        {
            BookingViewModels.Add(bookingViewModel);
            RaisePropertyChanged(nameof(Price));
        }

        /// <summary>
        /// Entfernt das übergebene BookingViewModel aus der Liste BookingViewModels
        /// </summary>
        /// <param name="bookingViewModel"></param>
        public void Remove(BookingViewModel bookingViewModel)
        {
            BookingViewModels.Remove(
            BookingViewModels.FirstOrDefault(b => b.Booking.Id == bookingViewModel.Booking.Id));
            RaisePropertyChanged(nameof(Price));
        }

        /// <summary>
        /// Zeigt an, ob in der list BookingViewModels mindestens 1 Element ist
        /// </summary>
        /// <returns></returns>
        public bool Any() => BookingViewModels.Any();

        /// <summary>
        /// Entfernt alle BookingViewModel aus der Liste BookingViewModels
        /// </summary>
        public void Clear()
        {
            BookingViewModels.Clear();
            RaisePropertyChanged(nameof(Price));
        }

        #endregion
    }
}
