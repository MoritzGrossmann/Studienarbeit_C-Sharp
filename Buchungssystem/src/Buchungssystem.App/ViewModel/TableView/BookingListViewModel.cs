using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel.TableView
{
    internal class BookingListViewModel : BaseViewModel
    {
        public BookingListViewModel(ICollection<Booking> bookings, Action<BookingViewModel> onBookingSelect)
        {
            _bookingViewModels = new ObservableCollection<BookingViewModel>(bookings.Select(b => new BookingViewModel(b, onBookingSelect)));
            _selectBooking = SelectBooking;
        }

        #region Properties

        private ObservableCollection<BookingViewModel> _bookingViewModels;

        public ObservableCollection<BookingViewModel> BookingViewModels
        {
            get => _bookingViewModels;
            set => _bookingViewModels = value;
        }

        public decimal Price
        {
            get
            {
                decimal sum = 0;
                _bookingViewModels.ForEach(bvm => sum += bvm.Booking.Product.Price);
                return sum;
            }
        }

        #endregion

        #region Actions

        private readonly Action<BookingViewModel> _selectBooking;

        private void SelectBooking(BookingViewModel bookingViewModel)
        {
            _selectBooking?.Invoke(bookingViewModel);
            RaisePropertyChanged(nameof(Price));
        }

        #region Actions for Commands

        private void Pay()
        {
            foreach (var bookingViewModel in BookingViewModels)
            {
                try
                {
                    // TODO
                }
                catch (Exception)
                {
                    // TODO
                }
            }
            BookingViewModels.Clear();

        }

        #endregion

        #endregion

        #region Commands

        public ICommand PayCommand { get; }

        public ICommand CancelCommand { get; }

        #endregion
    }
}
