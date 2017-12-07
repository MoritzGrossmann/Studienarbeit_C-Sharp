using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            set => _productViewModels = value;
        }

        public bool Any() => _productViewModels.Any();


        private readonly string _name;
        public string Name => _name;

        private readonly bool _showReturnButton;

        public bool ShowReturnButton => _showReturnButton;

        #endregion

        #region Contructor

        public ProductListViewModel(ProductGroup productGroup, ICollection<Product> products, EventHandler<Product> onProductSelect,
            EventHandler<ProductGroup> returnToProductGroup)
        {
            _name = productGroup.Name;
            _productViewModels = new ObservableCollection<ProductViewModel>(products.Select(p => new ProductViewModel(p, onProductSelect)));
            _showReturnButton = true;
            ReturnToParentCommand = new RelayCommand(() => returnToProductGroup?.Invoke(this,(ProductGroup)productGroup.Parent()));
        }

        public ProductListViewModel(ICollection<Product> products, EventHandler<Product> onProductSelect)
        {
            _productViewModels = new ObservableCollection<ProductViewModel>(products.Select(p => new ProductViewModel(p, onProductSelect)));
            _showReturnButton = false;
        }

        #endregion

        #region Commands

        public ICommand ReturnToParentCommand { get; }

        #endregion
    }
}
