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
            CurrentViewModel = new ChangeRoomsViewModel(_bookingSystemDataPersistence.Rooms());
        }

        #endregion

        #region Properties

        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel == value) return;
                _currentViewModel = value;
                RaisePropertyChanged(nameof(CurrentViewModel));
            }
        }

        #endregion

        #region Actions

        private void SetViewModel(BaseViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }

        #endregion
    }
}
