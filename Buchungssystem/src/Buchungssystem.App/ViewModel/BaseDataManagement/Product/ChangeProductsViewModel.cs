﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Product
{
    internal class ChangeProductsViewModel : BaseViewModel
    {
        #region Properties

        private readonly IPersistBookingSystemData _bookingSystemPersistence;

        public ObservableCollection<ProductViewModel> ProductViewModels { get; set; }

        private BaseViewModel _actualProductViewModel;

        public BaseViewModel ActualProductViewModel
        {
            get => _actualProductViewModel;
            set => SetProperty(ref _actualProductViewModel, value, nameof(ActualProductViewModel));
        }

        public ChangeProductsViewModel(IPersistBookingSystemData bookingSystemPersistence)
        {
            _bookingSystemPersistence = bookingSystemPersistence;
            ProductViewModels = new ObservableCollection<ProductViewModel>(_bookingSystemPersistence.Products().Select(p => new ProductViewModel(p, Select)));
            AddCommand = new RelayCommand(Add);

            ActualProductViewModel = ProductViewModels.Any() ? new CreateProductViewModel(Save, Delete, _bookingSystemPersistence, ProductViewModels.FirstOrDefault().Product) : null;
        }

        #endregion

        #region Actions

        public void Select(Domain.Model.Product product)
        {
            ActualProductViewModel = new CreateProductViewModel(Save, Delete, _bookingSystemPersistence, product);
        }

        public void Save(object sender, Domain.Model.Product product)
        {
            try
            {
                int oldId = product.Id;
                var p = product.Persist();

                if (p.Id != oldId)
                {
                    ProductViewModels.Add(new ProductViewModel(p, Select));
                    ActualProductViewModel = new CreateProductViewModel(Save, Delete, _bookingSystemPersistence, p);
                }
                else
                {
                    ProductViewModels.FirstOrDefault(pvm => pvm.Product.Id == product.Id).Product = p;
                }

            }
            catch (ModelExistException ex)
            {
                throw ex;
            }
        }

        public void Delete(object Sender, Domain.Model.Product product)
        {
            var productViewModel = ProductViewModels.FirstOrDefault(p => p.Product.Id == product.Id);
            ProductViewModels.Remove(productViewModel);
            ActualProductViewModel = ProductViewModels.Any() ? new CreateProductViewModel(Save, Delete, _bookingSystemPersistence, ProductViewModels.FirstOrDefault().Product) : new CreateProductViewModel(Save, _bookingSystemPersistence);

        }

        public void Add()
        {
            ActualProductViewModel = new CreateProductViewModel(Save, _bookingSystemPersistence);
        }

        #endregion

        #region Commands

        public ICommand AddCommand { get; }

        #endregion
    }
}