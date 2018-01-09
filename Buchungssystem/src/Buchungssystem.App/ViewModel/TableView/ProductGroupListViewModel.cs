using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.TableView
{
    internal class ProductGroupListViewModel : BaseViewModel
    {
        #region Properties

        private ObservableCollection<ProductGroupViewModel> _productGroupViewModels;

        public ObservableCollection<ProductGroupViewModel> ProductGroupViewModels
        {
            get => _productGroupViewModels;
            set => SetProperty(ref _productGroupViewModels, value, nameof(ProductGroupViewModels));
        }

        public bool HasParent { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, nameof(Name));
        }

        #endregion

        #region Constructor

        public ProductGroupListViewModel(string name, ICollection<ProductGroup> productGroups,
            Action<ProductGroup> onSelect, Action<ProductGroup> returnToParent)
        {
            ProductGroupViewModels = new ObservableCollection<ProductGroupViewModel>(productGroups.Select(p => new ProductGroupViewModel(p, onSelect)));
            HasParent = productGroups.Any(p => p.Parent() != null);
            Name = name;

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit productGroups.Any() ausgeschlossen
            ReturnToParentCommand = productGroups.Any() ? new RelayCommand(() => returnToParent?.Invoke((ProductGroup)productGroups.FirstOrDefault().Parent())) : null;
        }

        #endregion

        #region Commands

        public ICommand ReturnToParentCommand { get; }

        #endregion
    }
}