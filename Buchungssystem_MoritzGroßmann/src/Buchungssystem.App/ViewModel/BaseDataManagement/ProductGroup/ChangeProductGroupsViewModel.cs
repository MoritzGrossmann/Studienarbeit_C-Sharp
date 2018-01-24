using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.ProductGroup
{
    /// <summary>
    /// ViewModel für die Übersicht der Warengruppen-Verwaltung
    /// </summary>
    internal class ChangeProductGroupsViewModel : BaseViewModel
    {
        private IPersistBookingSystemData _bookingSystemPersistence;

        #region Constructor

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
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

        /// <summary>
        /// List von aller existenten Warengruppen 
        /// </summary>
        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels {
            get => _productGroupViewModels;
            set => SetProperty(ref _productGroupViewModels, value, nameof(ProductGroupViewModels));
        }

        private EditViewModel _actualProductGroupViewModel;

        /// <summary>
        /// Repräsentiert die aktuell bearbeitete Warengruppe
        /// </summary>
        public EditViewModel ActualProductGroupViewModel
        {
            get => _actualProductGroupViewModel;
            set => SetProperty(ref _actualProductGroupViewModel, value, nameof(ActualProductGroupViewModel));
        }

        #endregion

        #region Commands

        /// <summary>
        /// Kommando zum Hinzufügen einer neuen Warengruppe
        /// </summary>
        public ICommand AddCommand { get; }
        
        #endregion

        #region Actions
        
        /// <summary>
        /// Aktion, die beim Speichern einer Warengruppe aufgerufen wird
        /// Fügt der Liste ProductGroupViewModels ein neues hinzu, wenn eine Neue erstellt wurde oder Updated die geseicherte Warengruppe
        /// </summary>
        /// <param name="productGroup"></param>
        private void Save(Domain.Model.ProductGroup productGroup)
        {
            var productGroupViewModel = ProductGroupViewModels.FirstOrDefault(p => p.ProductGroup.Id == productGroup.Id);

            if (productGroupViewModel != null)
                productGroupViewModel.ProductGroup = productGroup;
            else
                ProductGroupViewModels.Add(new ProductGroupViewModel(productGroup, Select));

            ActualProductGroupViewModel = new EditProductGroupViewModel(Save, Delete, _bookingSystemPersistence, productGroup);
        }

        /// <summary>
        /// Aktion, die beim Löschen einer Warengruppe aufgerufen wird
        /// Löscht aus der Liste ProductViewModels das ProductViewModel der Warengruppe, welche gelöscht wurde
        /// </summary>
        /// <param name="productGroup"></param>
        private void Delete(Domain.Model.ProductGroup productGroup)
        {
            var productGroupViewModel = ProductGroupViewModels.FirstOrDefault(p => p.ProductGroup.Id == productGroup.Id);
            ProductGroupViewModels.Remove(productGroupViewModel);

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit RoomViewModels.Any() ausgeschlossen
            ActualProductGroupViewModel = ProductGroupViewModels.Any() ? new EditProductGroupViewModel(Save, Delete, _bookingSystemPersistence, ProductGroupViewModels.FirstOrDefault().ProductGroup) : new EditProductGroupViewModel(Save, _bookingSystemPersistence);
        }

        /// <summary>
        /// Aktion, die beim Anwählen einer Wasrengruppe ausgeführt wird
        /// Setzt das das ActualProductGroupViewModel auf ein neues EditProductGroupViewModel mit der Ausgewählten Warengruppe
        /// </summary>
        /// <param name="productGroup">Ausgewählte Warengruppe</param>
        private void Select(Domain.Model.ProductGroup productGroup)
        {
            ActualProductGroupViewModel = new EditProductGroupViewModel(Save, Delete, _bookingSystemPersistence, productGroup);
        }

        /// <summary>
        /// Setzt das ActualProductGroupViewModel auf ein neues EditProductGroupViewModel zum erstellen einer neuen Warengruppe
        /// </summary>
        private void AddProductGroup()
        {
            ActualProductGroupViewModel = new EditProductGroupViewModel(Save, _bookingSystemPersistence);
        }

        #endregion
    }
}
