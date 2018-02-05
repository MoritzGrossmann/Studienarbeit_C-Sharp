using System;
using System.Collections.Generic;
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
        #region Constructor

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
        public ChangeProductGroupsViewModel(IPersistBookingSystemData bookingSystemPersistence)
        {
            _bookingSystemPersistence = bookingSystemPersistence;
            _productGroups = bookingSystemPersistence.ProductGroups().OrderBy(p => p.Name).ToList();

            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(_productGroups.Select(p => new ProductGroupViewModel(p, Select)));

            ActualProductGroupViewModel = ProductGroupViewModels.Any()
                // ReSharper disable once PossibleNullReferenceException : KAnn nicht null sein, da ausgeführt wird wenn ProductGroupViewModels.Any()
                ? new EditProductGroupViewModel(Save,Delete, _bookingSystemPersistence, ProductGroupViewModels.FirstOrDefault().ProductGroup)
                : new EditProductGroupViewModel(Save, _bookingSystemPersistence);

            AddCommand = new RelayCommand(AddProductGroup);
        }


        #endregion

        #region Properties

        /// <summary>
        /// Datenbakkontext
        /// </summary>
        private readonly IPersistBookingSystemData _bookingSystemPersistence;

        /// <summary>
        /// Liste aller existierenden Warengruppen
        /// </summary>
        private ICollection<Domain.Model.ProductGroup> _productGroups;

        private ObservableCollection<ProductGroupViewModel> _productGroupViewModels;

        /// <summary>
        /// Repräsentiert alle angezeigten Waren
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


        private string _query = String.Empty;

        /// <summary>
        /// Repräsentiert den String in der Warensuche
        /// </summary>
        public string Query
        {
            get => _query;
            set
            {
                if (value.Trim().Equals(String.Empty))
                {
                    ProductGroupViewModels =
                        new ObservableCollection<ProductGroupViewModel>(
                            _productGroups.Select(p => new ProductGroupViewModel(p, Select)));
                }
                else
                {
                    ProductGroupViewModels =
                        new ObservableCollection<ProductGroupViewModel>(
                            _productGroups.Where(p => p.Name.ToLower().Contains(value.ToLower())).Select(p => new ProductGroupViewModel(p, Select)));
                }
                SetProperty(ref _query, value, nameof(Query));

            }
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
            if (_productGroups.Any(p => p.Id == productGroup.Id))
            {
                _productGroups.Remove(_productGroups.FirstOrDefault(p => p.Id == productGroup.Id));
            }

            _productGroups.Add(productGroup);
            _productGroups = _productGroups.OrderBy(p => p.Name).ToList();

            ProductGroupViewModels =
                new ObservableCollection<ProductGroupViewModel>(
                    Query.Trim().Equals(String.Empty)
                        ? _productGroups.Select(p => new ProductGroupViewModel(p, Select))
                        : _productGroups.Where(p => p.Name.ToLower().Contains(Query.ToLower())).Select(p => new ProductGroupViewModel(p, Select)));

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

            _productGroups.Remove(_productGroups.FirstOrDefault(p => p.Id == productGroup.Id));

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit RoomViewModels.Any() ausgeschlossen
            ActualProductGroupViewModel = _productGroups.Any() ? new EditProductGroupViewModel(Save, Delete, _bookingSystemPersistence, _productGroups.FirstOrDefault()) : new EditProductGroupViewModel(Save, _bookingSystemPersistence);
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
