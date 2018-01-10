using System;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Product
{
    internal class EditProductViewModel : EditViewModel
    {
        private readonly int _id;

        #region Properties

        private decimal _price;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        public decimal Price
        {
            get => _price;
            set
            {

                if (value < 0)
                {
                    AddError(nameof(Price), "Der Preis darf nicht negativ sein");
                    RaisePropertyChanged(nameof(HasErrors));
                }
                else
                {
                    RemoveError(nameof(Price));
                    RaisePropertyChanged(nameof(HasErrors));
                }


                SetProperty(ref _price, value, nameof(Price));
               
            }
        }

        private ProductGroupViewModel _productGroupViewModel;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        public ProductGroupViewModel ProductGroupViewModel
        {
            get => _productGroupViewModel;
            set => SetProperty(ref _productGroupViewModel, value, nameof(ProductGroupViewModel));
        }

        private readonly Domain.Model.Product _product;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels { get; }

        #endregion

        #region Constructors

        public EditProductViewModel(Action<Domain.Model.Product> onSave, IPersistBookingSystemData bookingSystemPersistence)
        {
            Edit = true;
            Name = "";
            Price = 0;
            BookingSystemPersistence = bookingSystemPersistence;
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(BookingSystemPersistence.LeafProductGroups().Select(p => new ProductGroupViewModel(p, null)));

            _onSave = onSave;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);

            AddError(nameof(Name), "Der Name darf nicht leer sein");
        }

        public EditProductViewModel(Action<Domain.Model.Product> onSave, Action<Domain.Model.Product> onDelete, IPersistBookingSystemData bookingSystemPersistence, Domain.Model.Product product)
        {
            _product = product;

            Edit = false;
            Name = product.Name;
            _id = product.Id;
            Price = product.Price;
            BookingSystemPersistence = bookingSystemPersistence;
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(BookingSystemPersistence.LeafProductGroups().Select(p => new ProductGroupViewModel(p, null)));

            _onSave = onSave;
            _onDelete = onDelete;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);

            ProductGroupViewModel =
                ProductGroupViewModels.FirstOrDefault(p => p.ProductGroup.Id == ((Domain.Model.ProductGroup) product.Parent()).Id);
        }

        #endregion

        #region Actions

        private void Save()
        {
            try
            {
                var product = new Domain.Model.Product()
                {
                    Id = _id,
                    Name = Name,
                    Persistence = BookingSystemPersistence,
                    Price = Price
                };

                product.SetParent(ProductGroupViewModel.ProductGroup);
                _onSave?.Invoke(product);
            }
            catch (ModelExistException)
            {
                AddError(nameof(Name), $"Der Name {Name} wurde schon vergeben");
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        private void Delete()
        {
            _product.Delete();
            _onDelete?.Invoke(_product);
        }

        private readonly Action<Domain.Model.Product> _onSave;

        private readonly Action<Domain.Model.Product> _onDelete;

        #endregion
    }
}