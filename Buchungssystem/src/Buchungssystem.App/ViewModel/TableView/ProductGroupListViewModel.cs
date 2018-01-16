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
    /// Repäsentiert eine Liste von ProductGroupViewModel
    /// </summary>
    internal class ProductGroupListViewModel : BaseViewModel
    {
        #region Properties

        private ObservableCollection<ProductGroupViewModel> _productGroupViewModels;

        /// <summary>
        /// ProductGroupViewModel im ListViewModel
        /// </summary>
        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels
        {
            get => _productGroupViewModels;
            set => SetProperty(ref _productGroupViewModels, value, nameof(ProductGroupViewModels));
        }

        /// <summary>
        /// Zeigt an, ob es eine Übergeordnete Warengruppe zu den angezeigten Warengruppe gibt 
        /// </summary>
        public bool HasParent { get; }

        private string _name;

        /// <summary>
        /// Name der Liste
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, nameof(Name));
        }

        private ProductGroup _parent;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productGroup">Warengruppe, welche angezeigt werden soll</param>
        /// <param name="productGroups">Warengruppen, die angezeigt werden. Alle Childelemente der Warengruppe productGroup</param>
        /// <param name="onSelect">Methode, die aufgerufen wird, wenn eine Warengruppe ausdgewählt wird</param>
        /// <param name="returnToParent">Methode, um die Eltern-Warengruppe anzuzeigen</param>

        public ProductGroupListViewModel(ProductGroup productGroup, ICollection<ProductGroup> productGroups,
            Action<ProductGroup> onSelect, Action<ProductGroup> returnToParent)
        {
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(productGroups.Select(p => new ProductGroupViewModel(p, onSelect)));
            HasParent = productGroups.Any(p => p.Parent() != null);
            Name = productGroup != null ? productGroup.Name : "Warengruppen";
            _parent = productGroup;
            _returnToParent = returnToParent;

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit productGroups.Any() ausgeschlossen
            //ReturnToParentCommand = productGroups.Any() ? new RelayCommand(() => returnToParent?.Invoke((ProductGroup)productGroups.FirstOrDefault().Parent())) : null;
            ReturnToParentCommand = new RelayCommand(ReturnToParent);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Kommando, um zur Parent-Liste zu gelangen
        /// </summary>
        public ICommand ReturnToParentCommand { get; }

        #endregion

        #region Actions

        private readonly Action<ProductGroup> _returnToParent;

        /// <summary>
        /// Führt die im Kontruktor übergebene Methode returnToParent aus und übergibt seinen eigenen Parent
        /// </summary>
        public void ReturnToParent()
        {
            _returnToParent?.Invoke(_parent);
        }

        #endregion
    }
}