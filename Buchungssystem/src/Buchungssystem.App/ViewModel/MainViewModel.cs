using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement;
using Buchungssystem.App.ViewModel.RoomView;
using Buchungssystem.Domain.Database;
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemDataPersistence;

        #region Constructor


        public MainViewModel()
        {
            _bookingSystemDataPersistence = new TestPersitence();

            _currentViewModel = new RoomListViewModel(_bookingSystemDataPersistence.Rooms());
        }


        public MainViewModel(IPersistBookingSystemData bookingSystemDataPersistence)
        {
            _bookingSystemDataPersistence = bookingSystemDataPersistence;

            _currentViewModel = new RoomListViewModel(_bookingSystemDataPersistence.Rooms());

            ToBaseDataCommand = new RelayCommand(ToBaseData);

            ToBookingCommand = new RelayCommand(ToBooking);
        }

        #endregion

        #region Properties

        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel.Equals(value)) return;
                _currentViewModel = value;
                RaisePropertyChanged(nameof(CurrentViewModel));
            }
        }

        #endregion

        #region Commands
   
        public ICommand ToBaseDataCommand {get;}

        public ICommand ToBookingCommand { get; }

        #endregion

        #region Actions

        private void ToBaseData()
        {
            //_dialogCoordinator.ShowMessageAsync(this, "fjrebhf", "gehe zu basedata");
            CurrentViewModel = new BaseDataManagementViewModel(_bookingSystemDataPersistence);
        }

        private void ToBooking()
        {
            CurrentViewModel = new RoomListViewModel(_bookingSystemDataPersistence.Rooms());
        }

        #endregion

    }
}
