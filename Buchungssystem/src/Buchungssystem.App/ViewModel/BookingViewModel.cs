using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    internal class BookingViewModel : BaseViewModel
    {
        private TableViewModel _tableViewModel;

        public TableViewModel TableViewModel
        {
            get => _tableViewModel;
            set => _tableViewModel = value;
        }

        private Booking _booking;
        public Booking Booking { get => _booking; }

        public void WaehleBuchung()
        {
            if (TableViewModel.SelectedBookings.Contains(this))
            {
                TableViewModel.SelectedBookings.Remove(this);
                TableViewModel.OpenBookings.Add(this);
            }
            else
            {
                TableViewModel.OpenBookings.Remove(this);
                TableViewModel.SelectedBookings.Add(this);
            }
        }

        public string Ware
        {
            get { return Booking.Product.Name; }
        }

        public decimal Preis
        {
            get { return Booking.Product.Price; }
        }

        public BookingViewModel(Booking booking, TableViewModel tableViewModel)
        {
            this._booking = booking;
            this._tableViewModel = tableViewModel;
        }
    }
}