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

        public ProductViewModel(Product product, Action<Product> onProductSelect)
        {
            _product = product;
            _onProductSelect = onProductSelect;
            SelectCommand = new RelayCommand(Select);
        }

        public ProductViewModel(Product product, Action<Product> onProductSelect, Action<Product> onProductBook)
        {
            _product = product;
            _onProductSelect = onProductSelect;
            _onProductBook = onProductBook;
            SelectCommand = new RelayCommand(Select);
            BookCommand = new RelayCommand(Book);
        }

        #endregion

        #region Properties

        private readonly Product _product;

        public string Name => _product.Name;

        public string Price => $"{decimal.Round(_product.Price, CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalDigits, MidpointRounding.AwayFromZero)} {CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol}";

        #endregion

        #region Commands

        public ICommand SelectCommand { get; }

        private void Select()
        {
            _onProductSelect?.Invoke(_product);
        }

        public ICommand BookCommand { get; }

        private void Book()
        {
            _onProductBook?.Invoke(_product);
        }

        #endregion

        #region Actions

        private readonly Action<Product> _onProductSelect;

        private readonly Action<Product> _onProductBook;

        #endregion
    }
}
