using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    internal class ProductGroupViewModel : BaseViewModel
    {
        private readonly IPersistBaseData _baseDataPersistence;

        private readonly IPersistBooking _bookingPersistence;

        #region Constructor

        public ProductGroupViewModel(IPersistBaseData baseDataPersistence, IPersistBooking bookingPersistence, ProductGroup productGroup, Action<ProductGroup> onProductGroupSelect)
        {
            _baseDataPersistence = baseDataPersistence;
            _bookingPersistence = bookingPersistence;
            _productGroup = productGroup;
            _onProductGroupSelect = onProductGroupSelect;

            _productCount = _baseDataPersistence.Products(_productGroup).Count;

            SelectCommand = new RelayCommand(Select);
        }

        #endregion

        #region Properties

        private ProductGroup _productGroup;

        public string Name => _productGroup.Name;

        public ProductGroup ProductGroup
        {
            get => _productGroup;
            set
            {
                if (_productGroup.Equals(value)) return;
                _productGroup = value;
                RaisePropertyChanged(nameof(ProductGroup));
            }
        }

        #endregion

        #region Commands

        public ICommand SelectCommand { get; }

        private readonly int _productCount;

        public string ProductCount => $"{_productCount} Waren";

        private void Select()
        {
            _onProductGroupSelect?.Invoke(_productGroup);
        }

        #endregion

        #region Actions

        private readonly Action<ProductGroup> _onProductGroupSelect;

        #endregion


    }
}