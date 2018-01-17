using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    /// <summary>
    /// Repräsentiert eine Warengruppe als ViewModel
    /// </summary>
    internal class ProductGroupViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productGroup">Warengruppe, welche gekapselt werden soll</param>
        /// <param name="onSelect">Methode, die aufgerufen wird, wenn eine Warengruppe ausgewählt wurde</param>
        public ProductGroupViewModel(ProductGroup productGroup, Action<ProductGroup> onSelect)
        {
            _productGroup = productGroup;
            SelectCommand = new RelayCommand(() => onSelect?.Invoke(_productGroup));
        }

        #endregion

        #region Properties

        private ProductGroup _productGroup;

        /// <summary>
        /// Warengruppe, welche gekapselt ist
        /// </summary>
        public ProductGroup ProductGroup
        {
            get => _productGroup;
            set => SetProperty(ref _productGroup, value, nameof(ProductGroup));
        }

        #endregion

        #region Commands

        /// <summary>
        /// Kommando zum Auswählen einer Warengruppe
        /// </summary>
        public ICommand SelectCommand { get; }

        #endregion
    }
}