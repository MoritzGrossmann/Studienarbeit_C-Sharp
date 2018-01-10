using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.ProductGroup
{
    internal class EditProductGroupViewModel : EditViewModel
    { 
        public int Id;

        private readonly Domain.Model.ProductGroup _productGroup;

        #region Constructor

        public EditProductGroupViewModel(Action<Domain.Model.ProductGroup> onSave,
            Action<Domain.Model.ProductGroup> onDelete, IPersistBookingSystemData bookingSystemPersistence,
            Domain.Model.ProductGroup productGroup)
        {
            BookingSystemPersistence = bookingSystemPersistence;
            BookingSystemPersistence = bookingSystemPersistence;
            _onSave = onSave;
            _onDelete = onDelete;

            _productGroup = productGroup;

            Id = productGroup.Id;
            Name = productGroup.Name;
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(bookingSystemPersistence.ProductGroups().Where(pg => pg.ChildNodes().All(c => c.GetType() != typeof(Domain.Model.Product))).Select(p => new ProductGroupViewModel(p,null)));

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
            BookingSystemPersistence = bookingSystemPersistence;
            _onSave = onSave;
            
            Name = String.Empty;
            Edit = true;

            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(bookingSystemPersistence.ProductGroups().Where(pg => pg.ChildNodes().All(c => c.GetType() != typeof(Domain.Model.Product))).Select(p => new ProductGroupViewModel(p, null)));

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

        public ProductGroupViewModel SelectedProductGroupViewModel
        {
            get => _selectedProductGroupViewModel;
            set => SetProperty(ref _selectedProductGroupViewModel, value, nameof(SelectedProductGroupViewModel));
        }

        private bool _noParent;

        public bool NoParent
        {
            get => _noParent;
            set => SetProperty(ref _noParent, value, nameof(NoParent));
        }

        #endregion

        #region Commands



        #endregion

        #region Actions

        private void Save()
        {
            var productGroup =
                new Domain.Model.ProductGroup() {Id = Id, Name = Name, Persistence = BookingSystemPersistence};
            productGroup.SetParent(SelectedProductGroupViewModel.ProductGroup ?? productGroup);

            productGroup = productGroup.Persist();

            if (NoParent)
                productGroup.SetParent(productGroup);

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
