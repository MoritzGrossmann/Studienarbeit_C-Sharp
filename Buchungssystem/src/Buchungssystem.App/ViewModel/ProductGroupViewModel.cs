using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    internal class ProductGroupViewModel : BaseViewModel
    {

        #region Constructor

        public ProductGroupViewModel(ProductGroup productGroup, EventHandler<ProductGroup> onProductGroupSelect)
        {
            _productGroup = productGroup;
            SelectCommand = new RelayCommand(() => onProductGroupSelect?.Invoke(this, _productGroup));
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

        #endregion
    }
}