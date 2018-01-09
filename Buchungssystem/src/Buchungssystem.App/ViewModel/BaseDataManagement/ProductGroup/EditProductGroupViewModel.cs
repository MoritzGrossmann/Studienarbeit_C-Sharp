using System;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.ProductGroup
{
    internal class EditProductGroupViewModel : EditViewModel
    { 
        public int Id;

        private Domain.Model.ProductGroup _productGroup;

        #region Constructor

        public EditProductGroupViewModel(Action<Domain.Model.ProductGroup> onSave,
            Action<Domain.Model.ProductGroup> onDelete, IPersistBookingSystemData bookingSystemPersistence,
            Domain.Model.ProductGroup productGroup)
        {
            _bookingSystemPersistence = bookingSystemPersistence;
            _onSave = onSave;
            _onDelete = onDelete;

            Id = productGroup.Id;
            Name = productGroup.Name;
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(bookingSystemPersistence.ProductGroups().Select(p => new ProductGroupViewModel(p,null)));

            var parent = (Domain.Model.ProductGroup) productGroup.Parent();

            SelectedProductGroupViewModel =
                ProductGroupViewModels.FirstOrDefault(pwm => pwm.ProductGroup.Id == parent.Id);

            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
            EditCommand = new RelayCommand(ToggleEdit);
        }

        public EditProductGroupViewModel(Action<Domain.Model.ProductGroup> onSave,
            IPersistBookingSystemData bookingSystemPersistence)
        {
            _bookingSystemPersistence = bookingSystemPersistence;
            _onSave = onSave;
            
            Name = String.Empty;
            Edit = true;

            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(bookingSystemPersistence.ProductGroups().Select(p => new ProductGroupViewModel(p, null)));

            if (ProductGroupViewModels.Any())
                SelectedProductGroupViewModel = ProductGroupViewModels.FirstOrDefault();

            SaveCommand = new RelayCommand(Save);
            DeleteCommand = new RelayCommand(Delete);
            EditCommand = new RelayCommand(ToggleEdit);
        }

        #endregion

        #region Properties

        private ObservableCollection<ProductGroupViewModel> _productGroupViewModels;

        /// <summary>
        /// Repräsentiert alle Produktgruppen die Existieren
        /// </summary>
        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels
        {
            get => _productGroupViewModels;
            set => SetProperty(ref _productGroupViewModels, value, nameof(ProductGroupViewModels));
        }

        private ProductGroupViewModel _selectedProductGroupViewModel;

        internal ProductGroupViewModel SelectedProductGroupViewModel
        {
            get => _selectedProductGroupViewModel;
            set => SetProperty(ref _selectedProductGroupViewModel, value, nameof(SelectedProductGroupViewModel));
        }

        #endregion

        #region Commands



        #endregion

        #region Actions

        private void Save()
        {
            var productGroup =
                new Domain.Model.ProductGroup() {Id = Id, Name = Name, Persistence = _bookingSystemPersistence};
            productGroup.SetParent(SelectedProductGroupViewModel.ProductGroup ?? productGroup);
            productGroup = productGroup.Persist();

            _onSave?.Invoke(productGroup);
        }

        private void Delete()
        {
            _productGroup.Delete();
            _onDelete(_productGroup);
        }

        private readonly Action<Domain.Model.ProductGroup> _onSave;

        private readonly Action<Domain.Model.ProductGroup> _onDelete;

        #endregion
    }
}
