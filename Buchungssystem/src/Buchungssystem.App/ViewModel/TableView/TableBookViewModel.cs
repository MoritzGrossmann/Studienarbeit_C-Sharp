using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository.Database;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel.TableView
{

    internal class TableBookViewModel : BaseViewModel
    {
        #region Properties

        private Table _table;

        public Table Table
        {
            get => _table;
            set => _table = value;
        }

        private BaseViewModel _sidebarViewModel;

        public BaseViewModel SidebarViewModel
        {
            get => _sidebarViewModel;
            set
            {
                _sidebarViewModel = value;
                RaisePropertyChanged(nameof(SidebarViewModel));
            } 
        }

        private bool _showSidebar;

        public bool SidebarIsShown
        {
            get { return _showSidebar; }
            set
            {
                _showSidebar = value;
                RaisePropertyChanged(nameof(SidebarIsShown));
                RaisePropertyChanged(nameof(SidebarIsntShown));
            }
        }

        public bool SidebarIsntShown => !_showSidebar;

        private string _sidebarHeaderText;

        public string SidebarHeaderText
        {
            get { return _sidebarHeaderText; }
            set { _sidebarHeaderText = value; RaisePropertyChanged(nameof(SidebarHeaderText));}
        }

        private BookingListViewModel _openBookings;

        public BookingListViewModel OpenBookings
        {
            get => _openBookings;
            set => _openBookings = value;
        }

        private BookingListViewModel _selectedBookings;

        public BookingListViewModel SelectedBookings
        {
            get => _selectedBookings;
            set => _selectedBookings = value;
        }

        public bool CanFinishBookings => SelectedBookings.Any();

        private ProductListViewModel _selectedProducts;

        public ProductListViewModel SelectedProducts
        {
            get => _selectedProducts;
            set => _selectedProducts = value;
        }

        public bool CanBookProducts => SelectedProducts.Any();


        private ICollection<ProductGroup> _productGroups;

        #endregion

        #region Constructor

        public TableBookViewModel(Table table, ICollection<ProductGroup> productGroups, Action onReturn)
        {
            Table = table;
            _productGroups = productGroups.OrderBy(p => p.Name).ToList();
            OpenBookings = new BookingListViewModel(table.Bookings.Where(b => b.Status == BookingStatus.Open).ToList(), SelectBooking);
            SelectedBookings = new BookingListViewModel(new List<Booking>(), SelectBooking);
            SelectedProducts = new ProductListViewModel(new List<Product>(), OnProductSelect, ShowProductGroups);
            SidebarViewModel = new ProductGroupListViewModel(_productGroups, OnProductGroupSelect);
            _sidebarHeaderText = "Warengruppen";

            ToTableListCommand = new RelayCommand(() => onReturn?.Invoke());
            PayCommand = new RelayCommand(PayBookings);
            CancelCommand = new RelayCommand(CancelBookings);
            ToogleSidebarCommand = new RelayCommand(() => SidebarIsShown = !SidebarIsShown);
            BookProductsCommand =  new RelayCommand(BookProducts);
        }

        #endregion

        #region Actions

        private void SelectBooking(BookingViewModel bookingViewModel)
        {
            if (OpenBookings.BookingViewModels.Contains(bookingViewModel))
            {
                OpenBookings.Remove(bookingViewModel);
                SelectedBookings.Add(bookingViewModel);
                RaisePropertyChanged(nameof(OpenBookings));
                RaisePropertyChanged(nameof(SelectedBookings));
            }
            else
            {
                SelectedBookings.Remove(bookingViewModel);
                OpenBookings.Add(bookingViewModel);
                RaisePropertyChanged(nameof(OpenBookings));
                RaisePropertyChanged(nameof(SelectedBookings));
            }

            RaisePropertyChanged(nameof(CanFinishBookings));
        }

        private void DeSelectBooking(BookingViewModel bookingViewModel)
        {
            SelectedBookings.BookingViewModels.Remove(SelectedBookings.BookingViewModels.FirstOrDefault(b => b.Booking.Id == bookingViewModel.Booking.Id));
            bookingViewModel.OnSelect = SelectBooking;
            OpenBookings.BookingViewModels.Add(bookingViewModel);
            RaisePropertyChanged(nameof(OpenBookings));
            RaisePropertyChanged(nameof(SelectedBookings));
            RaisePropertyChanged(nameof(OpenBookings.Price));
        }

        private void PayBookings()
        {
            SelectedBookings.BookingViewModels.ForEach(bvm => bvm.Booking.Pay());
            SelectedBookings.Clear();
            RaisePropertyChanged(nameof(CanFinishBookings));
        }

        private void CancelBookings()
        {
            SelectedBookings.BookingViewModels.ForEach(bvm => bvm.Booking.Cancel());
            SelectedBookings.Clear();
            RaisePropertyChanged(nameof(CanFinishBookings));
        }

        private void ShowProductGroups()
        {
            SidebarViewModel = new ProductGroupListViewModel(_productGroups, OnProductGroupSelect);
            SidebarHeaderText = "Warengruppen";
        }

        private void BookProducts()
        {
            foreach (var productViewModel in SelectedProducts.ProductViewModels)
            {
                var booking = new Booking()
                {
                    Persistence = Table.Persistence,
                    Product = productViewModel.Product,
                    Table = Table
                }.Persist();
                OpenBookings.Add(new BookingViewModel(booking, SelectBooking));
                Table.Bookings.Add(booking);
            }
            SelectedProducts.ProductViewModels.Clear();

            RaisePropertyChanged(nameof(CanBookProducts));
        }

        #endregion

        #region Commands

        public ICommand ToTableListCommand { get; }

        public ICommand PayCommand { get; }

        public ICommand CancelCommand { get; }

        public ICommand ToogleSidebarCommand { get; }

        public ICommand BookProductsCommand { get; set; }

        #endregion

        #region EventHandler

        private void OnProductGroupSelect(object sender, ProductGroup p)
        {
            SidebarViewModel = new ProductListViewModel(p.Products.OrderBy(pr => pr.Name).ToList(), OnProductSelect, ShowProductGroups);
            SidebarHeaderText = p.Name;
        }

        private void OnProductDeSelect(object sender, Product p)
        {
            SelectedProducts.ProductViewModels.Remove((ProductViewModel) sender);
            RaisePropertyChanged(nameof(CanBookProducts));
        }

        private void OnProductSelect(object sender, Product p)
        {
            SelectedProducts.ProductViewModels.Add(new ProductViewModel(p, OnProductDeSelect));
            RaisePropertyChanged(nameof(CanBookProducts));
        }

        #endregion
    }
}
