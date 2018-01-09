using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel.TableView
{
    internal class BookingListViewModel : BaseViewModel
    {
        public BookingListViewModel(ICollection<Booking> bookings, Action<BookingViewModel> onBookingSelect)
        {
            _bookingViewModels = new ObservableCollection<BookingViewModel>(bookings.Select(b => new BookingViewModel(b, SelectBooking)));
            _selectBooking = onBookingSelect;
        }

        #region Properties

        private ObservableCollection<BookingViewModel> _bookingViewModels;

        public ObservableCollection<BookingViewModel> BookingViewModels
        {
            get => _bookingViewModels;
            set => SetProperty(ref _bookingViewModels, value, nameof(BookingViewModels));
        }

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

        private void SelectBooking(BookingViewModel bookingViewModel)
        {
            _selectBooking?.Invoke(bookingViewModel);
            RaisePropertyChanged(nameof(BookingViewModels));
            RaisePropertyChanged(nameof(Price));
        }

        public void Add(BookingViewModel bookingViewModel)
        {
            BookingViewModels.Add(bookingViewModel);
            RaisePropertyChanged(nameof(Price));
        }

        public void Remove(BookingViewModel bookingViewModel)
        {
            BookingViewModels.Remove(
            BookingViewModels.FirstOrDefault(b => b.Booking.Id == bookingViewModel.Booking.Id));
            RaisePropertyChanged(nameof(Price));
        }

        public bool Any() => BookingViewModels.Any();

        public void Clear()
        {
            BookingViewModels.Clear();
            RaisePropertyChanged(nameof(Price));
        }

        #endregion
    }
}
