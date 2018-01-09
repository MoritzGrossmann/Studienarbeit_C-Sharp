using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    internal class ProductGroupViewModel : BaseViewModel
    {
        #region Constructor

        public ProductGroupViewModel(ProductGroup productGroup, Action<ProductGroup> onSelect)
        {
            _productGroup = productGroup;
            SelectCommand = new RelayCommand(() => onSelect?.Invoke(_productGroup));
        }

        #endregion

        #region Properties

        private ProductGroup _productGroup;

        public ProductGroup ProductGroup
        {
            get => _productGroup;
            set => SetProperty(ref _productGroup, value, nameof(ProductGroup));
        }

        #endregion

        #region Commands

        public ICommand SelectCommand { get; }

        #endregion
    }
}