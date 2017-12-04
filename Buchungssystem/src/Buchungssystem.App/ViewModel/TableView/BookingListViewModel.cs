using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
            _bookingViewModels = new ObservableCollection<BookingViewModel>(bookings.Select(b => new BookingViewModel(b, SelectBooking)));
            _selectBooking = onBookingSelect;
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

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value == -1) return;
                _selectedIndex = value; 
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
