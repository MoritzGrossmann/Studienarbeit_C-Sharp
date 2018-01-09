using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement.Product;
using Buchungssystem.App.ViewModel.BaseDataManagement.Room;
using Buchungssystem.Domain.Database;
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    internal class BaseDataManagementViewModel : BaseViewModel
    {
        #region Constructor

        public BaseDataManagementViewModel() : this(new TestPersitence())
        {
            
        }

        public BaseDataManagementViewModel(IPersistBookingSystemData bookingSystemDataPersistence)
        {
            var bookingSystemDataPersistence1 = bookingSystemDataPersistence;
            RoomsViewModel = new ChangeRoomsViewModel(bookingSystemDataPersistence1);
            ProductsViewModel = new ChangeProductsViewModel(bookingSystemDataPersistence1);
        }

        #endregion

        #region Properties

        public BaseViewModel RoomsViewModel { get; set; }

        public BaseViewModel ProductsViewModel { get; set; }

        #endregion
    }
}
