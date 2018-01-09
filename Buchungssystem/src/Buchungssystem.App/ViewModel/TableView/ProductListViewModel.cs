using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.TableView
{
    internal class ProductListViewModel : BaseViewModel
    {
        #region Properties

        private ObservableCollection<ProductViewModel> _productViewModels;

        public ObservableCollection<ProductViewModel> ProductViewModels
        {
            get => _productViewModels;
            set => SetProperty(ref _productViewModels, value, nameof(ProductViewModels));
        }

        public bool Any() => _productViewModels.Any();

        public string Name { get; }

        public bool ShowReturnButton { get; }

        #endregion

        #region Contructor

        public ProductListViewModel(ProductGroup productGroup, ICollection<Product> products, Action<Product> onProductSelect,
            Action<ProductGroup> returnToProductGroup)
        {
            Name = productGroup.Name;
            _productViewModels = new ObservableCollection<ProductViewModel>(products.Select(p => new ProductViewModel(p, onProductSelect)));
            ShowReturnButton = true;
            ReturnToParentCommand = new RelayCommand(() => returnToProductGroup?.Invoke((ProductGroup)productGroup.Parent()));
        }

        public ProductListViewModel(ICollection<Product> products, Action<Product> onProductSelect)
        {
            _productViewModels = new ObservableCollection<ProductViewModel>(products.Select(p => new ProductViewModel(p, onProductSelect)));
            ShowReturnButton = false;
        }

        #endregion

        #region Commands

        public ICommand ReturnToParentCommand { get; }

        #endregion
    }
}
