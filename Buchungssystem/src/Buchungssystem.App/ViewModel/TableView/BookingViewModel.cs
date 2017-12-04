using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.TableView
{
    internal class BookingViewModel : BaseViewModel
    {
        private readonly Booking _booking;

        public Booking Booking => _booking;

        public BookingViewModel(Booking booking)
        {
            _booking = booking;
        }

        public string BookedDate => Booking.Created.ToShortTimeString();

        public BookingViewModel(Booking booking, Action<BookingViewModel> onSelect)
        {
            OnSelect = onSelect;
            _booking = booking;
            SelectCommand = new RelayCommand(Select);
        }

        #region Commands

        public Action<BookingViewModel> OnSelect { get; set; }

        public ICommand SelectCommand { get; }

        private void Select()
        {
            OnSelect?.Invoke(this);
        }



        #endregion
    }
}