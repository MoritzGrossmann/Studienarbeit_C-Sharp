using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement;
using Buchungssystem.App.ViewModel.Loading;
using Buchungssystem.App.ViewModel.RoomView;
using Buchungssystem.Domain.Database;


namespace Buchungssystem.App.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemDataPersistence;

        #region Constructor

        public MainViewModel(IPersistBookingSystemData bookingSystemDataPersistence)
        {
            CurrentViewModel = new LoadingViewModel();

            _bookingSystemDataPersistence = bookingSystemDataPersistence;

            ToBookingCommand = new RelayCommand(ToBooking);

            ToggleFlyoutCommand = new RelayCommand(ToggleFlyout);

            ToBaseDataCommand = new RelayCommand(ToBaseData);

            TaskAwaiter<RoomListViewModel> awaiter = GetBookingViewModel().GetAwaiter();

            awaiter.OnCompleted(() => CurrentViewModel = awaiter.GetResult());
        }

        #endregion

        #region Properties

        private bool _showFlyout;

        public bool ShowFlyout
        {
            get => _showFlyout;
            set => SetProperty(ref _showFlyout, value, nameof(CurrentViewModel));
        }

        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value, nameof(CurrentViewModel));
        }

        #endregion

        #region Commands
   
        public ICommand ToBaseDataCommand {get;}

        public ICommand ToBookingCommand { get; }

        public ICommand ToggleFlyoutCommand { get; }

        #endregion

        #region Actions

        private void ToBaseData()
        {
            CurrentViewModel = new LoadingViewModel();
            TaskAwaiter<BaseDataManagementViewModel> awaiter = GetBaseDataManagementViewModel().GetAwaiter();

            awaiter.OnCompleted(() => CurrentViewModel = awaiter.GetResult());

        }

        private Task<BaseDataManagementViewModel> GetBaseDataManagementViewModel()
        {
            return Task.Run(() => new BaseDataManagementViewModel(_bookingSystemDataPersistence));
        }

        private void ToBooking()
        {
            CurrentViewModel = new LoadingViewModel();
            TaskAwaiter<RoomListViewModel> awaiter = GetBookingViewModel().GetAwaiter();

            awaiter.OnCompleted(() => CurrentViewModel = awaiter.GetResult());
        }

        private Task<RoomListViewModel> GetBookingViewModel()
        {
            return Task.Run(() => new RoomListViewModel(_bookingSystemDataPersistence.Rooms()));
        }

        private void ToggleFlyout()
        {
            ShowFlyout = !ShowFlyout;
        }
        #endregion

    }
}
