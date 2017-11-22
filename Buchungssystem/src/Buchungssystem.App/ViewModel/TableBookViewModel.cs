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
using Buchungssystem.Repository;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel
{
    internal class TableBookViewModel : BaseViewModel
    {
        private readonly IPersistBaseData _baseDataPersistence;

        private readonly IPersistBooking _bookingPersistence;


        #region Constructor

        public TableBookViewModel() : this(null, null, null)
        {

        }

        public TableBookViewModel(Table table, IPersistBaseData baseDataPersistence, IPersistBooking bookingPersistence)
        {
            _openBookings = new List<Booking>();
            _selectedBookings = new List<Booking>();
            _selectedProducts = new ObservableCollection<Product>();
            _productGroups = new List<ProductGroup>();

            _table = table;
            _baseDataPersistence = baseDataPersistence;
            _bookingPersistence = bookingPersistence;

            GetProductGroups();

            OpenBookings = table.Bookings;

            PayBookingsCommand = new RelayCommand(PayBookings); // TODO no relaycommand, only can executed, if Bookings are selected
            BookProductsCommand = new RelayCommand(BookProducts); // TODO no relaycommand, only can execute if products are selected
        }

        private void GetProductGroups()
        {
            _productGroups = _baseDataPersistence?.ProductGroups();
        }

        #endregion

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

        public string TableName => _table.Name;

        private List<ProductGroup> _productGroups;

        public ObservableCollection<ProductGroupViewModel> ProductGroups
        {
            get => new ObservableCollection<ProductGroupViewModel>(_productGroups.Select(p => new ProductGroupViewModel(p)));
        }

        private List<Booking> _openBookings;

        public List<Booking> OpenBookings
        {
            get => _openBookings;
            set
            {
                if (_openBookings.Equals(value)) return;
                _openBookings = value;
                RaisePropertyChanged(nameof(OpenBookingsViewModels));
            }
        }

        public ObservableCollection<BookingViewModel> OpenBookingsViewModels => new ObservableCollection<BookingViewModel>(_openBookings.Select(b => new BookingViewModel(b, SelectBooking)));

        private List<Booking> _selectedBookings = new List<Booking>();
        public List<Booking> SelectedBookings
        {
            get => _selectedBookings;
            set
            {
                if (_selectedBookings.Equals(value)) return;
                _selectedBookings = value;
                RaisePropertyChanged(nameof(SelectedBookingsViewModels));
            }
        }

        public ObservableCollection<BookingViewModel> SelectedBookingsViewModels => new ObservableCollection<BookingViewModel>(_selectedBookings.Select(b => new BookingViewModel(b, SelectBooking)));

        private ObservableCollection<Product> _selectedProducts = new ObservableCollection<Product>(new List<Product>());
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

        #endregion

        #region Commands

        public void SelectBooking(Booking booking)
        {
            if (OpenBookings.Contains(booking))
            {
                SelectedBookings.Add(booking);
                OpenBookings.Remove(booking);
            } else if (SelectedBookings.Contains(booking))
            {
                SelectedBookings.Remove(booking);
                OpenBookings.Add(booking);
            }

            RaisePropertyChanged(nameof(SelectedBookingsViewModels));
            RaisePropertyChanged(nameof(OpenBookingsViewModels));
        }

        public ICommand SelectProductCommand;

        private void SelectProduct(Product product)
        {
            SelectedProducts.Add(product);
        }

        public ICommand BookProductsCommand;

        private void BookProducts()
        {
            foreach (var product in _selectedProducts)
            {
                var booking = new Booking()
                {
                    TableId = Table.TableId,
                    ProductId = product.ProductId,
                    Product = product,
                    Table = Table,
                    Status = (int) BookingStatus.Open,
                    Timestamp = DateTime.Now
                };

                try
                {
                    _bookingPersistence.Book(booking);
                    OpenBookings.Add(booking);
                    SelectedProducts.Remove(product);
                }
                catch (Exception)
                {
                    // TODO
                }
            }
        }

        public ICommand PayBookingsCommand;

        public void PayBookings()
        {
            foreach (var booking in _selectedBookings)
            {
                try
                {
                    _bookingPersistence.Pay(booking);
                    SelectedBookings.Remove(booking);
                }
                catch (Exception)
                {
                    // TODO
                }
            }
        }

        #endregion
    }

    internal class ProductGroupViewModel : BaseViewModel
    {
        #region Constructor

        public ProductGroupViewModel(ProductGroup productGroup)
        {
            
        }

        #endregion

    }
}
