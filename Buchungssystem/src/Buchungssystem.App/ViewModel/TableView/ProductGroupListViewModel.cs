using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
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

        private readonly bool _hasParent;

        public bool HasParent => _hasParent;

        #endregion

        #region Contructor

        //public ProductGroupListViewModel(ProductGroup productGroup,
        //    EventHandler<ProductGroup> onProductGroupSelect, Action<ProductGroup> onReturnToParent)
        //{
        //    _productGroupViewModels = new ObservableCollection<ProductGroupViewModel>(productGroup.ChildNodes().Where(c => c.GetType() == typeof(ProductGroup)).Select(p => new ProductGroupViewModel((ProductGroup)p, onProductGroupSelect)));
        //    _hasParent = true;
        //    ReturnToParentCommand = new RelayCommand(() => onReturnToParent?.Invoke(productGroup));
        //}

        public ProductGroupListViewModel(ICollection<ProductGroup> productGroups,
            EventHandler<ProductGroup> onProductGroupSelect, Action<ProductGroup> returnToParent)
        {
            _productGroupViewModels = new ObservableCollection<ProductGroupViewModel>(productGroups.Select(p => new ProductGroupViewModel(p, onProductGroupSelect)));
            _hasParent = productGroups.Any(p => p.Parent() != null);
            ReturnToParentCommand = new RelayCommand(() => returnToParent?.Invoke((ProductGroup)productGroups.FirstOrDefault().Parent()));
        }

        #endregion

        #region Commands

        public ICommand ReturnToParentCommand { get; }

        #endregion
    }
}