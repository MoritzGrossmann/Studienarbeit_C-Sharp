using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Product
{
    /// <summary>
    /// ViewModel für die Übersicht der Waren-Verwaltung
    /// </summary>
    internal class ChangeProductsViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// Datenbankkontext
        /// </summary>
        private readonly IPersistBookingSystemData _bookingSystemPersistence;

        /// <summary>
        /// Liste aller existierenden Waren
        /// </summary>
        private ICollection<Domain.Model.Product> _products;

        private ObservableCollection<ProductViewModel> _productViewModels;

        /// <summary>
        /// Repräsentiert alle angezeigten Waren
        /// </summary>
        public ObservableCollection<ProductViewModel> ProductViewModels
        {
            get => _productViewModels;
            set => SetProperty(ref _productViewModels, value, nameof(ProductViewModels));
        }

        private BaseViewModel _actualProductViewModel;

        /// <summary>
        /// Repräsentiert die aktuell bearbeitete Ware
        /// </summary>
        public BaseViewModel ActualProductViewModel
        {
            get => _actualProductViewModel;
            set => SetProperty(ref _actualProductViewModel, value, nameof(ActualProductViewModel));
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
                    ProductViewModels =
                        new ObservableCollection<ProductViewModel>(
                            _products.Select(p => new ProductViewModel(p, Select)));
                }
                else
                {
                    ProductViewModels =
                        new ObservableCollection<ProductViewModel>(
                            _products.Where(p => p.Name.ToLower().Contains(value.ToLower())).Select(p => new ProductViewModel(p, Select)));
                }
                SetProperty(ref _query, value, nameof(Query));

            }
        }

        #endregion

        /// <summary>
        /// Standardkontruktor
        /// </summary>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
        public ChangeProductsViewModel(IPersistBookingSystemData bookingSystemPersistence)
        {
            _bookingSystemPersistence = bookingSystemPersistence;
            _products = _bookingSystemPersistence.Products().OrderBy(p => p.Name).ToList();

            ProductViewModels = new ObservableCollection<ProductViewModel>(_products.Select(p => new ProductViewModel(p, Select)));
            AddCommand = new RelayCommand(Add);

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit ProductViewModels.Any() ausgeschlossen
            ActualProductViewModel = ProductViewModels.Any() ? new EditProductViewModel(Save, Delete, _bookingSystemPersistence, ProductViewModels.FirstOrDefault().Product) : null;
        }


        #region Actions

        /// <summary>
        /// Aktion, die bei der Auswahl einer Ware ausgeführt wird
        /// </summary>
        /// <param name="product">Ausgewählte Ware</param>
        private void Select(Domain.Model.Product product)
        {
            ActualProductViewModel = new EditProductViewModel(Save, Delete, _bookingSystemPersistence, product);
        }

        /// <summary>
        /// Aktion, die beim Speichern einer Ware ausgeführt wird
        /// Speichert die Ware in der Datenbank
        /// Fügt der Liste ProductViewModels ein neues ProductViewModel hinzu wenn eine neue Ware angelegt wurde oder Updated das PRoductViewModel mit der entsprechenden Ware
        /// </summary>
        /// <param name="product">Gespeicherte Ware</param>
        private void Save(Domain.Model.Product product)
        {

            if (_products.Any(p => p.Id == product.Id))
            {
                _products.Remove(_products.FirstOrDefault(p => p.Id == product.Id));
            }

            _products.Add(product);
            _products = _products.OrderBy(p => p.Name).ToList();

            ProductViewModels = new ObservableCollection<ProductViewModel>(
                Query.Trim().Equals(String.Empty) 
                ? _products.Select(p => new ProductViewModel(p, Select)) 
                : _products.Where(p => p.Name.ToLower().Contains(Query.ToLower())).Select(p => new ProductViewModel(p, Select)));

            ActualProductViewModel = new EditProductViewModel(Save, Delete, _bookingSystemPersistence, product);
        }

        /// <summary>
        /// Aktion, die beim Löschen einer Ware ausgeführt wird
        /// Löscht aus der Liste ProductViewModels das ProductViewModel mit der gelöschten Ware
        /// </summary>
        /// <param name="product">Gelöschte Ware</param>
        private void Delete(Domain.Model.Product product)
        {
            var productViewModel = ProductViewModels.FirstOrDefault(p => p.Product.Id == product.Id);
            ProductViewModels.Remove(productViewModel);

            _products.Remove(_products.FirstOrDefault(p => p.Id == product.Id));

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit ProductViewModels.Any() ausgeschlossen
            ActualProductViewModel = _products.Any() ? new EditProductViewModel(Save, Delete, _bookingSystemPersistence, _products.FirstOrDefault()) : new EditProductViewModel(Save, _bookingSystemPersistence);

        }

        /// <summary>
        /// Anlegen einer neuen Ware
        /// ActualProductViewModel wird auf ein neues EditProductViewModel gesetzt
        /// </summary>
        private void Add()
        {
            ActualProductViewModel = new EditProductViewModel(Save, _bookingSystemPersistence);
        }

        #endregion

        #region Commands

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommando zun Anlegen einer neuen Ware
        /// </summary>
        public ICommand AddCommand { get; }

        #endregion
    }
}