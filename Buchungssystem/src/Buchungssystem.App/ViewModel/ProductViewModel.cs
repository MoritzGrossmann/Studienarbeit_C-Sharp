using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    internal class ProductViewModel : BaseViewModel
    {
        #region Contructor

        public ProductViewModel(Product product, EventHandler<Product> onProductSelect)
        {
            _product = product;
            SelectCommand = new RelayCommand(() => onProductSelect?.Invoke(this,_product));
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
