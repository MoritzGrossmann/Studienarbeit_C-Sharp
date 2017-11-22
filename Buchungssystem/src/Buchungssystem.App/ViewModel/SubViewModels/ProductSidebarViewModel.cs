using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.SubViewModels
{
    internal class ProductSidebarViewModel : BaseViewModel
    {
        #region Properties

        private readonly List<Product> _products;

        public ObservableCollection<ProductViewModel> Products => new ObservableCollection<ProductViewModel>(_products.Select(p => new ProductViewModel(p, _onProductSelect)));

        #endregion

        #region Constructor

        public ProductSidebarViewModel(List<Product> products, Action<Product> onProductSelect)
        {
            _products = products;
            _onProductSelect = onProductSelect;
            RaisePropertyChanged(nameof(Products));
        }

        #endregion

        #region Actions

        private readonly Action<Product> _onProductSelect;

        #endregion

        #region Commands


        #endregion
    }
}
