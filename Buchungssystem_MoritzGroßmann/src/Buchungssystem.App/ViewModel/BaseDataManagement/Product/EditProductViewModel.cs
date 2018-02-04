using System;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using MahApps.Metro.Controls.Dialogs;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Product
{
    /// <summary>
    /// ViewModel zum Erstellen und Editieren von Waren
    /// </summary>
    internal class EditProductViewModel : EditViewModel
    {
        private readonly int _id;

        #region Properties

        private decimal _price;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt

        /// <summary>
        /// Preis des Produktes in der jeweiligen Landeswährung
        /// </summary>
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

        /// <summary>
        /// Angewählte Warengruppe für die Ware
        /// </summary>
        public ProductGroupViewModel ProductGroupViewModel
        {
            get => _productGroupViewModel;
            set => SetProperty(ref _productGroupViewModel, value, nameof(ProductGroupViewModel));
        }

        private readonly Domain.Model.Product _product;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt

        /// <summary>
        /// Liste der Warengruppen, welche keine weiteren Warengruppen als Kinder hat
        /// </summary>
        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Kontruktor für das Erstellen einer neuen Ware
        /// </summary>
        /// <param name="onSave">Methode, die beim Speichern der Ware aufgerufen wird</param>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
        public EditProductViewModel(Action<Domain.Model.Product> onSave, IPersistBookingSystemData bookingSystemPersistence)
        {
            HeaderText = "Neue Ware Anelegen";
            Edit = true;
            Name = String.Empty;
            Price = 0;
            BookingSystemPersistence = bookingSystemPersistence;
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(BookingSystemPersistence.LeafProductGroups().Select(p => new ProductGroupViewModel(p, null)));

            _onSave = onSave;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);

            AddError(nameof(Name), "Der Name darf nicht leer sein");
        }

        /// <summary>
        /// Kontruktor für das Editieren einer vorhandenen Ware
        /// </summary>
        /// <param name="onSave">Methode, die beim Speichern der Ware aufgerufen wird</param>
        /// <param name="onDelete">Methode, die beim Löschen der Ware aufgerufen wird</param>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
        /// <param name="product">Die zu Editierende Ware</param>
        public EditProductViewModel(Action<Domain.Model.Product> onSave, Action<Domain.Model.Product> onDelete, IPersistBookingSystemData bookingSystemPersistence, Domain.Model.Product product)
        {
            HeaderText = $"{product.Name} bearbeiten";
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

        /// <summary>
        /// Speichert eine Ware und Ruft die im Kontruktor übergebene Funktion onSave auf
        /// </summary>
        private async void Save()
        {
            ShowProgressbar = true;

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

                var p = await product.Persist();

                _onSave?.Invoke(p);
            }
            catch (ModelExistException ex)
            {
                AddError(nameof(Name), ex.Message);
                RaisePropertyChanged(nameof(HasErrors));
            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Speichern der Ware", $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                ShowProgressbar = false;
            }
        }

        /// <summary>
        /// Löscht eine Ware und ruft die im Kontruktor übergebene Funktion onDelete auf
        /// </summary>
        private async void Delete()
        {
            var result = await DialogCoordinator.Instance.ShowMessageAsync(this, "Achtung",
                $"Wollen sie die Ware \"{_product.Name}\" wirklich löschen?", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                try
                {
                    await _product.Delete();
                    _onDelete?.Invoke(_product);
                }
                catch (Exception ex)
                {
                    await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Löschen der Ware", $"{ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private readonly Action<Domain.Model.Product> _onSave;

        private readonly Action<Domain.Model.Product> _onDelete;

        #endregion
    }
}