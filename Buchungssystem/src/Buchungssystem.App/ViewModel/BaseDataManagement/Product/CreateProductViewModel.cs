using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Product
{
    internal class CreateProductViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemPersistence;

        private int _id;

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (this._name != value)
                {
                    if (value.Trim().Equals(""))
                    {
                        base.AddError(nameof(Name), "Der Name darf nicht leer sein");
                        RaisePropertyChanged(nameof(HasErrors));
                    }
                    else
                    {
                        base.RemoveError(nameof(Name));
                        RaisePropertyChanged(nameof(HasErrors));
                    }

                    SetProperty(ref _name, value, nameof(Name));
                }
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                if (this._price != value)
                {
                    if (value < 0)
                    {
                        base.AddError(nameof(Price), "Der Preis darf nicht negativ sein");
                        RaisePropertyChanged(nameof(HasErrors));
                    }
                    else
                    {
                        base.RemoveError(nameof(Price));
                        RaisePropertyChanged(nameof(HasErrors));
                    }


                    SetProperty(ref _price, value, nameof(Price));
                }
            }
        }

        private ProductGroupViewModel _productGroupViewModel;
        public ProductGroupViewModel ProductGroupViewModel
        {
            get => _productGroupViewModel;
            set => SetProperty(ref _productGroupViewModel, value, nameof(ProductGroupViewModel));
        }

        private bool _edit;

        public bool Edit
        {
            get => _edit;
            set => SetProperty(ref _edit, value, nameof(Edit));
        }

        private Domain.Model.Product _product;

        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels { get; set; }

        public CreateProductViewModel(EventHandler<Domain.Model.Product> onSave, IPersistBookingSystemData bookingSystemPersistence)
        {
            Edit = true;
            Name = "";
            Price = 0;
            _bookingSystemPersistence = bookingSystemPersistence;
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(_bookingSystemPersistence.LeafProductGroups().Select(p => new ProductGroupViewModel(p, null)));
            _onSave = onSave;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);
        }

        public CreateProductViewModel(EventHandler<Domain.Model.Product> onSave, EventHandler<Domain.Model.Product> onDelete, IPersistBookingSystemData bookingSystemPersistence, Domain.Model.Product product)
        {
            _product = product;

            Edit = false;
            Name = product.Name;
            _id = product.Id;
            Price = product.Price;
            _bookingSystemPersistence = bookingSystemPersistence;
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(_bookingSystemPersistence.LeafProductGroups().Select(p => new ProductGroupViewModel(p, null)));

            _onSave = onSave;
            _onDelete = onDelete;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);

            ProductGroupViewModel =
                ProductGroupViewModels.FirstOrDefault(p => p.ProductGroup.Id == ((ProductGroup) product.Parent()).Id);
        }

        #region Commands

        public ICommand SaveCommand { get; }

        public ICommand EditCommand { get; }

        public ICommand DeleteCommand { get; set; }

        #endregion

        #region Actions

        private void ToggleEdit()
        {
            Edit = !Edit;
        }

        private void Save()
        {
            try
            {
                var product = new Domain.Model.Product()
                {
                    Id = _id,
                    Name = Name,
                    Persistence = _bookingSystemPersistence,
                    Price = Price
                };

                product.SetParent(ProductGroupViewModel.ProductGroup);
                _onSave?.Invoke(this, product);
            }
            catch (ModelExistException)
            {
                base.AddError(nameof(Name), $"Der Name {_name} wurde schon vergeben");
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        private void Delete()
        {
            _product.Delete();
            _onDelete?.Invoke(this, _product);
        }

        private readonly EventHandler<Domain.Model.Product> _onSave;

        private readonly EventHandler<Domain.Model.Product> _onDelete;

        #endregion
    }
}