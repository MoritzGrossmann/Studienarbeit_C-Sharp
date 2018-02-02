using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel.TableView
{
    /// <summary>
    /// ViewModel, welches für das Buchen von Waren auf Tische dient
    /// </summary>
    internal class TableBookViewModel : BaseViewModel
    {
        #region Properties

        private Table _table;

        /// <summary>
        /// Tisch, auf den Waren gebucht werden sollen
        /// </summary>
        public Table Table
        {
            get => _table;
            set => _table = value;
        }

        private BaseViewModel _sidebarViewModel;

        /// <summary>
        /// View, welche an der Seite angezeigt wird. zum Beispiel ein ProductrGroup- oder ProductListViewModel
        /// </summary>
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

        /// <summary>
        /// Zeigt an, ob diue Sidebar angezeigt wird
        /// </summary>
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

        /// <summary>
        /// Headertext der Sidebar
        /// </summary>
        public string SidebarHeaderText
        {
            get { return _sidebarHeaderText; }
            set { _sidebarHeaderText = value; RaisePropertyChanged(nameof(SidebarHeaderText));}
        }

        private BookingListViewModel _openBookings;

        /// <summary>
        /// Liste der Offenen Buchungen auf einem Tisch
        /// </summary>
        public BookingListViewModel OpenBookings
        {
            get => _openBookings;
            set => _openBookings = value;
        }

        private BookingListViewModel _selectedBookings;

        /// <summary>
        /// Liste der Buchungen, welche zum Bezahlen oder Stornieren angewählt wurden
        /// </summary>
        public BookingListViewModel SelectedBookings
        {
            get => _selectedBookings;
            set => _selectedBookings = value;
        }

        /// <summary>
        /// Zeigt an, ob Buchungen Bezahlt oder Stoerniert werden können.
        /// Dies ist der Fall, falls die Liste der Buchungen in den SelectedBookings nicht leer ist
        /// </summary>
        public bool CanFinishBookings => SelectedBookings.Any();

        private ProductListViewModel _selectedProducts;

        /// <summary>
        /// Waren, welche zum Buchunen auf einen Tisch ausgewählt wurden
        /// </summary>
        public ProductListViewModel SelectedProducts
        {
            get => _selectedProducts;
            set => _selectedProducts = value;
        }

        /// <summary>
        /// Zeigt an, ob Waren auf den Tisch gebucht werden können.
        /// Dies ist der Fall wenn angewählte Waren existieren
        /// </summary>
        public bool CanBookProducts => SelectedProducts.Any();


        private ICollection<ProductGroup> _productGroups;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table">Tisch, auf den Waren gebucht werden sollen</param>
        /// <param name="productGroups">alle Wurzel-Warengruppen</param>
        /// <param name="onReturn">Methode, die ausgeführt wird, wenn man zur Raumansicht zurückkehrten will</param>
        public TableBookViewModel(Table table, ICollection<ProductGroup> productGroups, Action onReturn)
        {
            Table = table;
            _productGroups = productGroups.OrderBy(p => p.Name).ToList();
            OpenBookings = new BookingListViewModel(table.Bookings.Where(b => b.Status == BookingStatus.Open).ToList(), SelectBooking);
            SelectedBookings = new BookingListViewModel(new List<Booking>(), SelectBooking);
            SelectedProducts = new ProductListViewModel(new List<Product>(), OnProductSelect);
            SidebarViewModel = new ProductGroupListViewModel(null, _productGroups, OnProductGroupSelect, ShowParent);

            ToTableListCommand = new RelayCommand(() => onReturn?.Invoke());
            PayCommand = new RelayCommand(PayBookings);
            CancelCommand = new RelayCommand(CancelBookings);
            ToogleSidebarCommand = new RelayCommand(() => SidebarIsShown = !SidebarIsShown);
            BookProductsCommand =  new RelayCommand(BookProducts);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Kommando, um zur Raumansiocht zurückzukehren
        /// </summary>
        public ICommand ToTableListCommand { get; }

        /// <summary>
        /// Kommando zum Bezahlen der ausgewählten Buchungen
        /// </summary>
        public ICommand PayCommand { get; }

        /// <summary>
        /// Kommando zum Stornieren der Ausgewählten Buchungen
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Kommando zum Verstecken und Anzeigen des SidebarViewModel
        /// </summary>
        public ICommand ToogleSidebarCommand { get; }

        /// <summary>
        /// Kommando zum Buchen der ausgewählten Waren
        /// </summary>
        public ICommand BookProductsCommand { get; }

        #endregion

        #region Actions

        /// <summary>
        /// Im SidebarViewModel werden alle Kind-Warengruppen, oder wenn keine Existieren alle Kind-Waren, der Übergebenen Warengruppe angezeigt
        /// </summary>
        /// <param name="productGroup">Warengruppe, welche angezeigt werden soll</param>
        private void OnProductGroupSelect(ProductGroup productGroup)
        {
            var productGroups = productGroup.ChildNodes().Where(c => c.GetType() == typeof(ProductGroup)).AsEnumerable().Select(pg => (ProductGroup)pg).ToList();

            if (productGroups.Any())
            {
                SidebarViewModel = new ProductGroupListViewModel(productGroup, productGroups, OnProductGroupSelect, ShowParent);
            }
            else
            {
                SidebarViewModel = new ProductListViewModel(productGroup,
                    productGroup.ChildNodes().Where(c => c.GetType() == typeof(Product)).AsEnumerable().Select(c => (Product) c)
                        .ToList(), OnProductSelect, ShowParent);
            }

            SidebarHeaderText = productGroup.Name;
        }

        /// <summary>
        /// Entfernt das ProductViewModel der Ware aus den Angewählten Waren
        /// </summary>
        /// <param name="product">Ware, welche entfernt werden soll</param>
        private void OnProductDeSelect(Product product)
        {
            var productViewModel = SelectedProducts.ProductViewModels.FirstOrDefault(p => p.Product.Id == product.Id);
            SelectedProducts.ProductViewModels.Remove(productViewModel);

            RaisePropertyChanged(nameof(CanBookProducts));
        }

        /// <summary>
        /// Fügt den Angewählten Waren ein ProductViewModel mit der übergebenen Ware hinzu
        /// </summary>
        /// <param name="product">Ware, welche hinzugefügt werden soll</param>
        private void OnProductSelect(Product product)
        {
            SelectedProducts.ProductViewModels.Add(new ProductViewModel(product, OnProductDeSelect));
            RaisePropertyChanged(nameof(CanBookProducts));
        }

        /// <summary>
        /// Verschiebt die Übergebene Buchung von den Offenen Buchungen in die Angewählten Buchungen und umgekehrt
        /// </summary>
        /// <param name="bookingViewModel"></param>
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

        /// <summary>
        /// Alle angewählten Buchungen werden als Bezahlt markiert und aus den Angewählten Buchungen gelöscht
        /// </summary>
        private void PayBookings()
        {
            SelectedBookings.BookingViewModels.ForEach(async bvm => await bvm.Booking.Pay());
            SelectedBookings.Clear();
            RaisePropertyChanged(nameof(CanFinishBookings));
        }

        /// <summary>
        /// Alle angewählten Buchungen werden als Stoerniert markiert und aus den Angewählten Buchungen gelöscht
        /// </summary>
        private void CancelBookings()
        {
            SelectedBookings.BookingViewModels.ForEach(async bvm => await bvm.Booking.Cancel());
            SelectedBookings.Clear();
            RaisePropertyChanged(nameof(CanFinishBookings));
        }

        /// <summary>
        /// Im SidebarViewModel wird das ProductGroupListViewModel des Parents der übergebenen Warengruppe angezeogt
        /// </summary>
        /// <param name="productGroup"></param>
        private void ShowParent(ProductGroup productGroup)
        {
            try
            {
                var parent = (ProductGroup)productGroup.Parent();

                if (parent.Id == productGroup.Id)
                {
                    SidebarViewModel =
                        new ProductGroupListViewModel(null, _productGroups, OnProductGroupSelect, ShowParent);
                }
                else
                {
                    SidebarViewModel = new ProductGroupListViewModel(
                        parent,
                        parent.ChildNodes()
                            .Where(c => c.GetType() == typeof(ProductGroup))
                            .AsEnumerable()
                            .Select(c => (ProductGroup)c)
                            .ToList(), OnProductGroupSelect, ShowParent);
                    SidebarHeaderText = parent.Name;
                }
            }
            catch (NullReferenceException)
            {
                SidebarViewModel = new ProductGroupListViewModel(null, _productGroups, OnProductGroupSelect, ShowParent);
            }
        }

        /// <summary>
        /// Von jeder angewählten Ware wird eine Buchung erstellt und den Offenen Buchungen hinzugefügt
        /// </summary>
        private async void BookProducts()
        {
            foreach (var productViewModel in SelectedProducts.ProductViewModels)
            {
                var booking = await new Booking()
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
    }
}
