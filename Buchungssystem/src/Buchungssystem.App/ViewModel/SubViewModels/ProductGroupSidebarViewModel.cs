using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel.SubViewModels
{
    internal class ProductGroupSidebarViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemDataPersistence;

        private readonly IPersistBooking _bookingPersistence;

        #region Properties

        private readonly List<ProductGroup> _productGroups;

        public ObservableCollection<ProductGroupViewModel> ProductGroups
        {
            get => new ObservableCollection<ProductGroupViewModel>(_productGroups.Select(p => new ProductGroupViewModel(_bookingSystemDataPersistence, _bookingPersistence, p, _onProductGroupSelect)));
        }

        #endregion

        #region Contructor

        public ProductGroupSidebarViewModel(IPersistBookingSystemData bookingSystemDataPerisstence, IPersistBooking bookingPersistence, Action<ProductGroup> onPRoductGroupSelect)
        {
            _bookingSystemDataPersistence = bookingSystemDataPerisstence;
            _bookingPersistence = bookingPersistence;

            _productGroups = bookingSystemDataPerisstence.ProductGroups();
            _onProductGroupSelect = onPRoductGroupSelect;

            RaisePropertyChanged(nameof(ProductGroups));
        }

        public ProductGroupSidebarViewModel()
        {
            _bookingSystemDataPersistence = new TestPersitence();
            _bookingPersistence = new TestPersitence();

            _productGroups = _bookingSystemDataPersistence.ProductGroups();

            _onProductGroupSelect = null;
            RaisePropertyChanged(nameof(ProductGroups));
        }

        #endregion

        #region Actions

        private readonly Action<ProductGroup> _onProductGroupSelect;

        #endregion
    }
}
