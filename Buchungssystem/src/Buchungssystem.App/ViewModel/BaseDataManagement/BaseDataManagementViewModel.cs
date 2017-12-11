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
            ChangeRoomsViewModel = new ChangeRoomsViewModel(_bookingSystemDataPersistence.Rooms(), _bookingSystemDataPersistence);
        }

        #endregion

        #region Properties

        private ChangeRoomsViewModel _changeRoomsViewModel;

        public ChangeRoomsViewModel ChangeRoomsViewModel
        {
            get => _changeRoomsViewModel;
            set => SetProperty(ref _changeRoomsViewModel, value, nameof(ChangeRoomsViewModel));
        }

        #endregion

        #region Actions

        #endregion
    }
}
