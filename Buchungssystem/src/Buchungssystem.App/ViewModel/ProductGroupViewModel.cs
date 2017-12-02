using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    internal class ProductGroupViewModel : BaseViewModel
    {

        #region Constructor

        public ProductGroupViewModel(ProductGroup productGroup, Action<ProductGroup> onProductGroupSelect)
        {
            _productGroup = productGroup;
            _onProductGroupSelect = onProductGroupSelect;

            _productCount = _productGroup.Products.Count;

            SelectCommand = new RelayCommand(Select);
        }

        #endregion

        #region Properties

        private ProductGroup _productGroup;

        public ProductGroup ProductGroup
        {
            get => _productGroup;
            set { _productGroup = value; }
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