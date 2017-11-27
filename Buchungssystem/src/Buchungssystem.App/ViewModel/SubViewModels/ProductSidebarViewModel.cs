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
        private readonly IPersistBaseData _baseDataPersistence;

        private readonly IPersistBooking _bookingPersistence;

        #region Properties

        private readonly List<Product> _products;

        public ObservableCollection<ProductViewModel> Products => new ObservableCollection<ProductViewModel>(_products.Select(p => new ProductViewModel(p, _onProductSelect)));

        private readonly ProductGroup _productGroup;

        public string ProductGroup => _productGroup.Name;

        #endregion

        #region Constructor

        public ProductSidebarViewModel(IPersistBaseData baseDataPersistence, IPersistBooking bookingPersistence, ProductGroup productGroup, Action<Product> onProductSelect, Action onRevert)
        {
            _baseDataPersistence = baseDataPersistence;
            _bookingPersistence = bookingPersistence;
            _productGroup = productGroup;

            RaisePropertyChanged(nameof(ProductGroup));

            _products = _baseDataPersistence.Products(productGroup);

            _onProductSelect = onProductSelect;
            _onRevert = onRevert;

            RaisePropertyChanged(nameof(Products));

            RevertCommand = new RelayCommand(Revert);
        }

        public ProductSidebarViewModel()
        {
            _baseDataPersistence = new TestPersitence();
            _bookingPersistence = new TestPersitence();
            _productGroup = _baseDataPersistence.ProductGroups().FirstOrDefault();
            _products = _baseDataPersistence.Products(_productGroup);

            _onProductSelect = null;
            RaisePropertyChanged(nameof(Products));
        }

        #endregion

        #region Actions

        private readonly Action<Product> _onProductSelect;

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
