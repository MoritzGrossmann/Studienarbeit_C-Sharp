using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Product
{
    internal class CreateProductViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemPersistence;

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductGroupViewModel ProductGroupViewModel { get; set; }

        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels { get; set; }

        private string _errorText;

        public string ErrorText
        {
            get => _errorText;
            set => SetProperty(ref _errorText, value, nameof(ErrorText));
        }

        private bool _hasErrors;

        public bool HasErrors
        {
            get => _hasErrors;
            set => SetProperty(ref _hasErrors, value, nameof(Name));
        }

        public CreateProductViewModel(EventHandler<Domain.Model.Product> onSave,
            IPersistBookingSystemData bookingSystemPersistence)
        {
            Name = "";
            Price = 0;
            _bookingSystemPersistence = bookingSystemPersistence;
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(_bookingSystemPersistence.LeafProductGroups().Select(p => new ProductGroupViewModel(p, SelectProductGroup)));
            _onSave = onSave;
            SaveCommand = new RelayCommand(Save);
        }

        #region Commands

        public ICommand SaveCommand { get; }

        #endregion

        #region Actions

        private void SelectProductGroup(object sender, ProductGroup productGroup)
        {
            
        }

        private void Save()
        {
            HasErrors = false;
            try
            {
                var product = new Domain.Model.Product()
                {
                    Name = Name,
                    Persistence = _bookingSystemPersistence,
                    Price = Price
                };

                product.SetParent(ProductGroupViewModel.ProductGroup);
                _onSave?.Invoke(this, product);
            }
            catch (ModelExistException modelExistException)
            {
                ErrorText = modelExistException.Message;
                HasErrors = true;
            }
        }

        private readonly EventHandler<Domain.Model.Product> _onSave;

    #endregion
    }
}