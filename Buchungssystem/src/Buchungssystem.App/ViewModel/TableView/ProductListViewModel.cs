using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.TableView
{
    /// <summary>
    /// Repräsentiert eine Liste von ProductVIewModel
    /// </summary>
    internal class ProductListViewModel : BaseViewModel
    {
        private ProductGroup _parent;

        #region Properties

        private ObservableCollection<ProductViewModel> _productViewModels;

        /// <summary>
        /// ProductVIewModel im ListVIewModel
        /// </summary>
        public ObservableCollection<ProductViewModel> ProductViewModels
        {
            get => _productViewModels;
            set => SetProperty(ref _productViewModels, value, nameof(ProductViewModels));
        }

        /// <summary>
        /// Zeigt an, ob in der Liste ProductVIewModel vorhanden sind
        /// </summary>
        /// <returns></returns>
        public bool Any() => _productViewModels.Any();

        /// <summary>
        /// Name der Warengruppe, zu der die ProductViewModel der Liste gehören
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Zeigt an, ob der Button zum zuürkckehren zum Parent angezeigt wird
        /// </summary>
        public bool ShowReturnButton { get; }

        #endregion

        #region Contructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productGroup">Warengruppe, zu denen die ProductViewModels gehören</param>
        /// <param name="products">Waren, welche angezeigt werden sollen</param>
        /// <param name="onProductSelect">Methode, die aufgerufen wird, wenn eine Ware ausgewählt wurde</param>
        /// <param name="returnToParent">Methode, die aufgerufen wird, wenn die Eltern-Warengrupppe angezeigt werden soll</param>
        public ProductListViewModel(ProductGroup productGroup, ICollection<Product> products, Action<Product> onProductSelect,
            Action<ProductGroup> returnToParent)
        {
            Name = productGroup.Name;
            _productViewModels = new ObservableCollection<ProductViewModel>(products.Select(p => new ProductViewModel(p, onProductSelect)));
            _returnToParent = returnToParent;
            _parent = productGroup;
            ShowReturnButton = true;
            ReturnToParentCommand = new RelayCommand(ReturnToParent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="products">Warne, die angezeigt werden sollen</param>
        /// <param name="onProductSelect">Methode, die aufgerufen wird, wenn eine Ware ausgewählt wurde</param>
        public ProductListViewModel(ICollection<Product> products, Action<Product> onProductSelect)
        {
            _productViewModels = new ObservableCollection<ProductViewModel>(products.Select(p => new ProductViewModel(p, onProductSelect)));
            ShowReturnButton = false;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Kommando, um zur Parent-Liste zu gelangen
        /// </summary>
        public ICommand ReturnToParentCommand { get; }

        #endregion

        #region Actions

        /// <summary>
        /// Führt die im Kontruktor übergebene Methode returnToParent aus und übergibt seinen eigenen Parent
        /// </summary>
        private void ReturnToParent()
        {
            _returnToParent?.Invoke(_parent);
        }

        private readonly Action<ProductGroup> _returnToParent;

        #endregion
    }
}
