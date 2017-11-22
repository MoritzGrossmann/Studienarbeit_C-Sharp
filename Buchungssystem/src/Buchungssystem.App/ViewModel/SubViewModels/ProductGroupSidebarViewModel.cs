using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.SubViewModels
{
    internal class ProductGroupSidebarViewModel : BaseViewModel
    {
        #region Properties

        private readonly List<ProductGroup> _productGroups;

        public ObservableCollection<ProductGroupViewModel> ProductGroups
        {
            get => new ObservableCollection<ProductGroupViewModel>(_productGroups.Select(p => new ProductGroupViewModel(p, _onProductGroupSelect)));
        }

        #endregion

        #region Contructor

        public ProductGroupSidebarViewModel(List<ProductGroup> productGroups, Action<ProductGroup> onPRoductGroupSelect)
        {
            _productGroups = productGroups;
            _onProductGroupSelect = onPRoductGroupSelect;
            RaisePropertyChanged(nameof(ProductGroups));
        }

        public ProductGroupSidebarViewModel() : this(null, null)
        {
            
        }

        #endregion

        #region Actions

        private readonly Action<ProductGroup> _onProductGroupSelect;

        #endregion
    }
}
