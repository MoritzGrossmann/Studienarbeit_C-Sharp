using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.TableView
{
    internal class ProductGroupListViewModel : BaseViewModel
    {
        #region Properties

        private ObservableCollection<ProductGroupViewModel> _productGroupViewModels;

        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels
        {
            get { return _productGroupViewModels; }
            set { _productGroupViewModels = value; }
        }

        #endregion

        #region Contructor

        public ProductGroupListViewModel(ICollection<ProductGroup> productGroups,
            EventHandler<ProductGroup> onProductGroupSelect)
        {
            _productGroupViewModels = new ObservableCollection<ProductGroupViewModel>(productGroups.Select(p => new ProductGroupViewModel(p, onProductGroupSelect)));
        }

        #endregion
    }
}