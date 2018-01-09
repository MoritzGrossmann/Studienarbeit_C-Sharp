using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Product
{
    internal class ProductViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// Repräsentiert das Produkt in einem ProduktViewModel
        /// </summary>
        public Domain.Model.Product Product { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Konstruktor für das Auswählen eines Produktes
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
        /// Kommando bei Auswahl eines Produktes
        /// </summary>
        public ICommand SelectCommand { get; }

        #endregion
    }
}
