using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
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