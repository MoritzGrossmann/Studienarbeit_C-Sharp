using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    internal class ProductViewModel : BaseViewModel
    {
        #region Contructor

        public ProductViewModel(Product product, Action<Product> onProductSelect)
        {
            _product = product;
            SelectCommand = new RelayCommand(() => onProductSelect?.Invoke(_product));
        }

        #endregion

        #region Properties

        private Product _product;

        public Product Product
        {
            get => _product;
            set => _product = value;
        }

        #endregion

        #region Commands

        public ICommand SelectCommand { get; }

        #endregion

    }
}
