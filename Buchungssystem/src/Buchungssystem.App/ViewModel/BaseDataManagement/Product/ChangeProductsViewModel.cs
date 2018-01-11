using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        private readonly IPersistBookingSystemData _bookingSystemPersistence;

        /// <summary>
        /// Repräsentiert alle existenten Waren
        /// </summary>
        public ObservableCollection<ProductViewModel> ProductViewModels { get; set; }

        private BaseViewModel _actualProductViewModel;

        /// <summary>
        /// Repräsentiert die aktuell bearbeitete Ware
        /// </summary>
        public BaseViewModel ActualProductViewModel
        {
            get => _actualProductViewModel;
            set => SetProperty(ref _actualProductViewModel, value, nameof(ActualProductViewModel));
        }

        #endregion

        /// <summary>
        /// Standardkontruktor
        /// </summary>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
        public ChangeProductsViewModel(IPersistBookingSystemData bookingSystemPersistence)
        {
            _bookingSystemPersistence = bookingSystemPersistence;
            ProductViewModels = new ObservableCollection<ProductViewModel>(_bookingSystemPersistence.Products().OrderBy(p => p.Name).Select(p => new ProductViewModel(p, Select)));
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

            int oldId = product.Id;

            TaskAwaiter<Domain.Model.Product> awaiter = SaveTask(product).GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                var p = product.Persist();

                if (p.Id != oldId)
                {
                    ProductViewModels.Add(new ProductViewModel(p, Select));
                    ActualProductViewModel = new EditProductViewModel(Save, Delete, _bookingSystemPersistence, p);
                }
                else
                    // ReSharper disable once PossibleNullReferenceException
                    ProductViewModels.FirstOrDefault(pvm => pvm.Product.Id == product.Id).Product = p;
            });
        }


        private Task<Domain.Model.Product> SaveTask(Domain.Model.Product product)
        {
            return Task.Run(() => product.Persist());
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

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit ProductViewModels.Any() ausgeschlossen
            ActualProductViewModel = ProductViewModels.Any() ? new EditProductViewModel(Save, Delete, _bookingSystemPersistence, ProductViewModels.FirstOrDefault().Product) : new EditProductViewModel(Save, _bookingSystemPersistence);

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