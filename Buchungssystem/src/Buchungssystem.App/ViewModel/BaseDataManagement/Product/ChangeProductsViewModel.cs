using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Product
{
    internal class ChangeProductsViewModel : BaseViewModel
    {
        #region Properties

        private readonly IPersistBookingSystemData _bookingSystemPersistence;

        public ObservableCollection<ProductViewModel> ProductViewModels { get; set; }

        private BaseViewModel _actualProductViewModel;

        public BaseViewModel ActualProductViewModel
        {
            get => _actualProductViewModel;
            set => SetProperty(ref _actualProductViewModel, value, nameof(ActualProductViewModel));
        }

#endregion
        public ChangeProductsViewModel(IPersistBookingSystemData bookingSystemPersistence)
        {
            _bookingSystemPersistence = bookingSystemPersistence;
            ProductViewModels = new ObservableCollection<ProductViewModel>(_bookingSystemPersistence.Products().OrderBy(p => p.Name).Select(p => new ProductViewModel(p, Select)));
            AddCommand = new RelayCommand(Add);

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit ProductViewModels.Any() ausgeschlossen
            ActualProductViewModel = ProductViewModels.Any() ? new EditProductViewModel(Save, Delete, _bookingSystemPersistence, ProductViewModels.FirstOrDefault().Product) : null;
        }


        #region Actions

        private void Select(Domain.Model.Product product)
        {
            ActualProductViewModel = new EditProductViewModel(Save, Delete, _bookingSystemPersistence, product);
        }

        private void Save(Domain.Model.Product product)
        {

            int oldId = product.Id;
            var p = product.Persist();

            if (p.Id != oldId)
            {
                ProductViewModels.Add(new ProductViewModel(p, Select));
                ActualProductViewModel = new EditProductViewModel(Save, Delete, _bookingSystemPersistence, p);
            }
            else
                // ReSharper disable once PossibleNullReferenceException
                ProductViewModels.FirstOrDefault(pvm => pvm.Product.Id == product.Id).Product = p;
        }

        private void Delete(Domain.Model.Product product)
        {
            var productViewModel = ProductViewModels.FirstOrDefault(p => p.Product.Id == product.Id);
            ProductViewModels.Remove(productViewModel);

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit ProductViewModels.Any() ausgeschlossen
            ActualProductViewModel = ProductViewModels.Any() ? new EditProductViewModel(Save, Delete, _bookingSystemPersistence, ProductViewModels.FirstOrDefault().Product) : new EditProductViewModel(Save, _bookingSystemPersistence);

        }

        private void Add()
        {
            ActualProductViewModel = new EditProductViewModel(Save, _bookingSystemPersistence);
        }

        #endregion

        #region Commands

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public ICommand AddCommand { get; }

        #endregion
    }
}