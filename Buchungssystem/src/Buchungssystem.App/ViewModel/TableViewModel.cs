using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Buchungssystem.App.Converter;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel
{
    internal class TableViewModel : BaseViewModel
    {

        #region Properties

        private Table _table;
        public Table Table
        {
            get => _table;
            set
            {
                if (_table.Equals(value)) return;
                _table = value;
                RaisePropertyChanged(nameof(Table));
            }
        }

        public string Name
        {
            get => _table.Name;
            set
            {
                if (_table.Name.Equals(value)) return;
                _table.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private ObservableCollection<BookingViewModel> _openBookings;
        public ObservableCollection<BookingViewModel> OpenBookings
        {
            get => _openBookings;
            set
            {
                if (_openBookings.Equals(value)) return;
                _openBookings = value;
                RaisePropertyChanged(nameof(OpenBookings));
                RaisePropertyChanged(nameof(InUse));
                RaisePropertyChanged(nameof(Color));
            }
        }

        private ObservableCollection<BookingViewModel> _selectedBookings;
        public ObservableCollection<BookingViewModel> SelectedBookings
        {
            get => _selectedBookings;
            set
            {
                if (_selectedBookings.Equals(value)) return;
                _selectedBookings = value;
                RaisePropertyChanged(nameof(SelectedBookings));
            }
        }

        private ObservableCollection<Product> _selectedProducts;
        public ObservableCollection<Product> SelectedProducts
        {
            get => _selectedProducts;
            set
            {
                if (_selectedProducts.Equals(value)) return;
                _selectedProducts = value;
                RaisePropertyChanged(nameof(SelectedProducts));
            }
        }

        public void BookProducts()
        {
            foreach (var ware in SelectedProducts)
            {
                OpenBookings.Add(new BookingViewModel(new BookingPersistence().Book(new Booking() {TableId = Table.TableId, Product = ware, Timestamp = DateTime.Now}), this));
                SelectedProducts.Remove(ware);
            }
        }

        public string VerbleibenderPreis
        {
            get
            {
                decimal sum = 0;
                _openBookings.ForEach(b => sum += b.Preis);
                var culture = CultureInfo.CurrentCulture;
                return
                    $"{decimal.Round(sum, culture.NumberFormat.CurrencyDecimalDigits, MidpointRounding.AwayFromZero)} {culture.NumberFormat.CurrencySymbol}";
            }
        }

        public decimal SelectedBookingsPrice
        {
            get
            {
                decimal sum = 0;
                SelectedBookings.ForEach(b => sum += b.Preis);
                return sum;
            }
        }

        public string LastBooking
        {
            get
            {
                if (_openBookings.Any())
                {
                    return _openBookings.Last().Booking.Timestamp.ToString(CultureInfo.CurrentCulture);
                }
                return "Keine Buchungen";
            }
        }

        public bool InUse => _openBookings.Any();

        public Brush Color => 
            InUse ? (Brush)new BrushConverter().ConvertFrom("#FFE6E6") : (Brush)new BrushConverter().ConvertFrom("#CCFFCC");

        private bool _selected;

        public bool Selected
        {
            get => _selected;
            set
            {
                if (_selected.Equals(value)) return;
                _selected = value;
                RaisePropertyChanged(nameof(Selected));
            }
        }

        #endregion

        public void Pay()
        {
            var buchungPersistenz = new BookingPersistence();
            _selectedBookings.ForEach(b => buchungPersistenz.Pay(b.Booking));
            _selectedBookings.Clear();
        }

        public void Cancle()
        {
            var buchungPersistenz = new BookingPersistence();
            _selectedBookings.ForEach(b => buchungPersistenz.Cancel(b.Booking));
            _selectedBookings.Clear();
        }

        #region Contructor
        public TableViewModel(Table table)
        {
            _table = table;
            _selectedBookings = new ObservableCollection<BookingViewModel>();
            _openBookings = new ObservableCollection<BookingViewModel>(new BookingPersistence().Bookings(table).Select(b => new BookingViewModel(b, this)));
            SelectCommand = new RelayCommand(Select);
        }

        public TableViewModel()
        {
            
        }

        #endregion

        public ICommand SelectCommand;

        public void Select()
        {
            this.Selected = true;
        }
    }
}
