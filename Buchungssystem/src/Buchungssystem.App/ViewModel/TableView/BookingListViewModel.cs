using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.TableView
{
    internal class BookingListViewModel : BaseViewModel
    {
        private readonly IPersistBooking _bookingPersistence;

        public BookingListViewModel(IPersistBooking bookingPersistence, List<BookingViewModel> bookingViewModels,
            Action<BookingViewModel> selectBooking)
        {
            _bookingPersistence = bookingPersistence;
            _bookingViewModels = new ObservableCollection<BookingViewModel>(bookingViewModels);
            _selectBooking = SelectBooking;
        }

        #region Properties

        private ObservableCollection<BookingViewModel> _bookingViewModels;

        public ObservableCollection<BookingViewModel> BookingViewModels
        {
            get => _bookingViewModels;
            set => _bookingViewModels = value;
        }

        #endregion

        #region Actions

        private readonly Action<BookingViewModel> _selectBooking;

        private void SelectBooking(BookingViewModel bookingViewModel)
        {
            BookingViewModels.Remove(bookingViewModel);
            _selectBooking?.Invoke(bookingViewModel);
        }

        #region Actions for Commands

        private void Pay()
        {
            foreach (var bookingViewModel in BookingViewModels)
            {
                try
                {
                    _bookingPersistence.Pay(bookingViewModel.Booking);
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
