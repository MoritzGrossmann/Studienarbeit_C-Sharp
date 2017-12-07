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

        #endregion

        #region Contructor

        public ProductListViewModel(ICollection<Product> products, EventHandler<Product> onProductSelect,
            Action returnToProductGroups)
        {
            _productViewModels = new ObservableCollection<ProductViewModel>(products.Select(p => new ProductViewModel(p, onProductSelect)));
            ReturnCommand = new RelayCommand(() => returnToProductGroups?.Invoke());
        }

        #endregion

        #region Commands

        public ICommand ReturnCommand { get; }

        #endregion
    }
}
