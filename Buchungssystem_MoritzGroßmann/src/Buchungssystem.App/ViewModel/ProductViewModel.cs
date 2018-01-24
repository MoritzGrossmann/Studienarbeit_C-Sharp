using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    /// <summary>
    /// Repräsentiert eine Ware als ViewModel
    /// </summary>
    internal class ProductViewModel : BaseViewModel
    {
        #region Contructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product">Ware, welche gekapselt werden soll</param>
        /// <param name="onSelect">Methode, die aufgerufen wird, wenn eine Ware ausgewählt wurde</param>
        public ProductViewModel(Product product, Action<Product> onSelect)
        {
            _product = product;
            SelectCommand = new RelayCommand(() => onSelect?.Invoke(_product));
        }

        #endregion

        #region Properties

        private Product _product;

        /// <summary>
        /// Ware, welche gekapselt ist
        /// </summary>
        public Product Product
        {
            get => _product;
            set => _product = value;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Kommando zum Auswählen einer Ware
        /// </summary>
        public ICommand SelectCommand { get; }

        #endregion

    }
}
