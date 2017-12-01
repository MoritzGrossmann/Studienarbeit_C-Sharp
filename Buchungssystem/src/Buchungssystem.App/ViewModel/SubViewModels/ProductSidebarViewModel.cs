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
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel.SubViewModels
{
    internal class ProductSidebarViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemDataPersistence;

        private readonly IPersistBooking _bookingPersistence;

        #region Properties

        private readonly List<Product> _products;

        public ObservableCollection<ProductViewModel> Products => new ObservableCollection<ProductViewModel>(_products.Select(p => new ProductViewModel(p, _onProductSelect, _onProductBook)));

        private readonly ProductGroup _productGroup;

        public string ProductGroup => _productGroup.Name;

        #endregion

        #region Constructor

        public ProductSidebarViewModel(IPersistBookingSystemData bookingSystemDataPersistence, IPersistBooking bookingPersistence, ProductGroup productGroup, Action<Product> onProductSelect, Action<Product> onProductBook, Action onRevert)
        {
            _bookingSystemDataPersistence = bookingSystemDataPersistence;
            _bookingPersistence = bookingPersistence;
            _productGroup = productGroup;

            RaisePropertyChanged(nameof(ProductGroup));

            _products = _bookingSystemDataPersistence.Products(productGroup);

            _onProductSelect = onProductSelect;
            _onProductBook = onProductBook;
            _onRevert = onRevert;

            RaisePropertyChanged(nameof(Products));

            RevertCommand = new RelayCommand(Revert);
        }

        public ProductSidebarViewModel()
        {
            _bookingSystemDataPersistence = new TestPersitence();
            _bookingPersistence = new TestPersitence();
            _productGroup = _bookingSystemDataPersistence.ProductGroups().FirstOrDefault();
            _products = _bookingSystemDataPersistence.Products(_productGroup);

            _onProductSelect = null;
            RaisePropertyChanged(nameof(Products));
        }

        #endregion

        #region Actions

        private readonly Action<Product> _onProductSelect;

        private readonly Action<Product> _onProductBook;

        private readonly Action _onRevert;

        #endregion

        #region Commands

        public ICommand RevertCommand { get; }

        private void Revert()
        {
            _onRevert?.Invoke();
        }

        #endregion
    }
}
