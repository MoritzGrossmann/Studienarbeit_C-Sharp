using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
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
            RoomsViewModel = new ChangeRoomsViewModel(_bookingSystemDataPersistence.Rooms(), _bookingSystemDataPersistence);
        }

        #endregion

        #region Properties

        private BaseViewModel _roomsViewModel;

        public BaseViewModel RoomsViewModel
        {
            get => _roomsViewModel;
            set => SetProperty(ref _roomsViewModel, value, nameof(RoomsViewModel));
        }

        #endregion
    }
}
