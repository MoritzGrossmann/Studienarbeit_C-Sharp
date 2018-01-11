using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.ProductGroup
{
    internal class ChangeProductGroupsViewModel : BaseViewModel
    {
        private IPersistBookingSystemData _bookingSystemPersistence;

        #region Constructor

        public ChangeProductGroupsViewModel(IPersistBookingSystemData bookingSystemPersistence)
        {
            _bookingSystemPersistence = bookingSystemPersistence;

            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(_bookingSystemPersistence.ProductGroups().OrderBy(p => p.Name).Select(p => new ProductGroupViewModel(p, Select)));

            ActualProductGroupViewModel = ProductGroupViewModels.Any()
                // ReSharper disable once PossibleNullReferenceException : KAnn nicht null sein, da ausgeführt wird wenn ProductGroupViewModels.Any()
                ? new EditProductGroupViewModel(Save,Delete, _bookingSystemPersistence, ProductGroupViewModels.FirstOrDefault().ProductGroup)
                : new EditProductGroupViewModel(Save, _bookingSystemPersistence);

            AddCommand = new RelayCommand(AddProductGroup);
        }

        #endregion

        #region Properties

        private ObservableCollection<ProductGroupViewModel> _productGroupViewModels;

        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels {
            get => _productGroupViewModels;
            set => SetProperty(ref _productGroupViewModels, value, nameof(ProductGroupViewModels));
        }

        private EditViewModel _actualProductGroupViewModel;

        public EditViewModel ActualProductGroupViewModel
        {
            get => _actualProductGroupViewModel;
            set => SetProperty(ref _actualProductGroupViewModel, value, nameof(ActualProductGroupViewModel));
        }

        #endregion

        #region Commands

        public ICommand AddCommand { get; }
        
        #endregion

        #region Actionss

        private void Save(Domain.Model.ProductGroup productGroup)
        {
            var productGroupViewModel = ProductGroupViewModels.FirstOrDefault(p => p.ProductGroup.Id == productGroup.Id);

            if (productGroupViewModel != null)
                productGroupViewModel.ProductGroup = productGroup;
            else
                ProductGroupViewModels.Add(new ProductGroupViewModel(productGroup, Select));

            ActualProductGroupViewModel = new EditProductGroupViewModel(Save, Delete, _bookingSystemPersistence, productGroup);
        }

        private void Delete(Domain.Model.ProductGroup productGroup)
        {
            var productGroupViewModel = ProductGroupViewModels.FirstOrDefault(p => p.ProductGroup.Id == productGroup.Id);
            ProductGroupViewModels.Remove(productGroupViewModel);

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit RoomViewModels.Any() ausgeschlossen
            ActualProductGroupViewModel = ProductGroupViewModels.Any() ? new EditProductGroupViewModel(Save, Delete, _bookingSystemPersistence, ProductGroupViewModels.FirstOrDefault().ProductGroup) : new EditProductGroupViewModel(Save, _bookingSystemPersistence);
        }

        private void Select(Domain.Model.ProductGroup productGroup)
        {
            ActualProductGroupViewModel = new EditProductGroupViewModel(Save, Delete, _bookingSystemPersistence, productGroup);
        }

        private void AddProductGroup()
        {
            ActualProductGroupViewModel = new EditProductGroupViewModel(Save, _bookingSystemPersistence);
        }

        #endregion
    }
}
