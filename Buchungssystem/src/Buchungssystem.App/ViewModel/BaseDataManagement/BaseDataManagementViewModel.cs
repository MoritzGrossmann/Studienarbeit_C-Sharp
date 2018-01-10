using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement.Product;
using Buchungssystem.App.ViewModel.BaseDataManagement.ProductGroup;
using Buchungssystem.App.ViewModel.BaseDataManagement.Room;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    internal class BaseDataManagementViewModel : BaseViewModel
    {
        #region Constructor

        public BaseDataManagementViewModel(IPersistBookingSystemData bookingSystemDataPersistence)
        {
            RoomsViewModel = new ChangeRoomsViewModel(bookingSystemDataPersistence);
            ProductGroupsViewModel = new ChangeProductGroupsViewModel(bookingSystemDataPersistence);
            ProductsViewModel = new ChangeProductsViewModel(bookingSystemDataPersistence);
        }

        #endregion

        #region Properties

        public BaseViewModel RoomsViewModel { get; set; }

        public BaseViewModel ProductsViewModel { get; set; }

        public BaseViewModel ProductGroupsViewModel { get; set; }

        #endregion
    }
}
