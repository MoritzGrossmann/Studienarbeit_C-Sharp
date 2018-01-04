using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement.Product;
using Buchungssystem.App.ViewModel.BaseDataManagement.Room;
using Buchungssystem.Domain.Database;
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    internal class BaseDataManagementViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemDataPersistence;

        #region Constructor

        public BaseDataManagementViewModel() : this(new TestPersitence())
        {
            
        }

        public BaseDataManagementViewModel(IPersistBookingSystemData bookingSystemDataPersistence)
        {
            _bookingSystemDataPersistence = bookingSystemDataPersistence;
            RoomsViewModel = new ChangeRoomsViewModel(_bookingSystemDataPersistence);
            ProductsViewModel = new ChangeProductsViewModel(_bookingSystemDataPersistence);
        }

        #endregion

        #region Properties

        public BaseViewModel RoomsViewModel { get; set; }

        public BaseViewModel ProductsViewModel { get; set; }

        #endregion
    }
}
