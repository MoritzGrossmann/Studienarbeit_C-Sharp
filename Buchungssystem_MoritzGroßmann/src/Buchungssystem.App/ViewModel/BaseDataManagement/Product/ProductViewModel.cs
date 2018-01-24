using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Product
{
    /// <summary>
    /// ViewModel zur Anzeige einer Ware in der Warenliste der Stammdatenverwaltung
    /// </summary>
    internal class ProductViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// Repräsentiert die Ware in einem ProduktViewModel
        /// </summary>
        public Domain.Model.Product Product { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Konstruktor für das Auswählen einer Ware
        /// </summary>
        /// <param name="product"></param>
        /// <param name="onSelect"></param>
        public ProductViewModel(Domain.Model.Product product, Action<Domain.Model.Product> onSelect)
        {
            Product = product;
            _select = onSelect;
            SelectCommand = new RelayCommand(Select);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Selectiert eine Ware
        /// Ruft die im Kontruktor übergebene Funktion onSelect auf
        /// </summary>
        private void Select()
        {
            _select?.Invoke(Product);
        }

        private readonly Action<Domain.Model.Product> _select;

        #endregion

        #region Commands

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Command zur Auswahl einer Ware
        /// </summary>
        public ICommand SelectCommand { get; }

        #endregion
    }
}
