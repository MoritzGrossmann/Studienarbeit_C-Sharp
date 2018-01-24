using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement;
using Buchungssystem.App.ViewModel.Bookings;
using Buchungssystem.App.ViewModel.Loading;
using Buchungssystem.App.ViewModel.RoomView;
using Buchungssystem.Domain.Database;


namespace Buchungssystem.App.ViewModel
{
    /// <summary>
    /// HauptViewModel, welches das Gesamte Programmfenster Repräsentiert
    /// </summary>
    internal class MainViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemDataPersistence;

        #region Constructor

        /// <summary>
        /// Standardkontruktore
        /// </summary>
        /// <param name="bookingSystemDataPersistence">Kontext ztur Datenbank</param>
        public MainViewModel(IPersistBookingSystemData bookingSystemDataPersistence)
        {
            ShowFlyout = false;

            Loading = true;

            CurrentViewModel = new LoadingViewModel();

            _bookingSystemDataPersistence = bookingSystemDataPersistence;

            ToBookingCommand = new RelayCommand(ToBooking);

            ToggleFlyoutCommand = new RelayCommand(ToggleFlyout);

            ToBaseDataCommand = new RelayCommand(ToBaseData);

            ToOverviewCommand = new RelayCommand(ToOverview);

            TaskAwaiter<RoomListViewModel> awaiter = GetBookingViewModel().GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                CurrentViewModel = awaiter.GetResult();
                Loading = false;
            });
        }

        #endregion

        #region Properties

        private bool _showFlyout;

        /// <summary>
        /// Zeit an, ob das Flyout angezeigt wird
        /// </summary>
        public bool ShowFlyout
        {
            get => _showFlyout;
            set => SetProperty(ref _showFlyout, value, nameof(ShowFlyout));
        }

        private BaseViewModel _currentViewModel;

        /// <summary>
        /// Das Aktuell angezeigte ViewMdoel
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value, nameof(CurrentViewModel));
        }

        private bool _loading;

        /// <summary>
        /// Zeigt an, ob gerade geladen wird
        /// </summary>
        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value, nameof(Loading));
        }

        #endregion

        #region Commands
   
        /// <summary>
        /// Kommando, um die Stammdatenverwaltung anzuzeigen
        /// </summary>
        public ICommand ToBaseDataCommand {get;}

        /// <summary>
        /// Kommando, um die Buchungsansicht anzuzeigen
        /// </summary>
        public ICommand ToBookingCommand { get; }

        /// <summary>
        /// Kommando zum anzeigen oder ausblenden des Flyouts
        /// </summary>
        public ICommand ToggleFlyoutCommand { get; }

        /// <summary>
        /// Kommando, um die Tagesübersicht anzuzeigen
        /// </summary>
        public ICommand ToOverviewCommand { get; }

        #endregion

        #region Actions

        /// <summary>
        /// Setzt ShowFlyout auf False
        /// Setzut das CurrentViewModel auf ein neues BaseDataManagementViewModel
        /// </summary>
        private void ToBaseData()
        {
            ShowFlyout = false;

            Loading = true;

            CurrentViewModel = new LoadingViewModel();
            TaskAwaiter<BaseDataManagementViewModel> awaiter = GetBaseDataManagementViewModel().GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                CurrentViewModel = awaiter.GetResult();
                Loading = false;
            });

        }

        private Task<BaseDataManagementViewModel> GetBaseDataManagementViewModel()
        {
            return Task.Run(() => new BaseDataManagementViewModel(_bookingSystemDataPersistence));
        }

        /// <summary>
        /// Setzt ShowFlyout auf False
        /// Setzt das CurrentViewModel auf ein neues RoomListViewModel
        /// </summary>
        private void ToBooking()
        {
            ShowFlyout = false;

            Loading = true;

            CurrentViewModel = new LoadingViewModel();
            TaskAwaiter<RoomListViewModel> awaiter = GetBookingViewModel().GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                CurrentViewModel = awaiter.GetResult();
                Loading = false;
            });
        }

        /// <summary>
        /// Setzt ShowFlyout auf False
        /// Setzt das CurrentViewModel auf ein neues BookingsFromDayViewModel
        /// </summary>
        private void ToOverview()
        {
            ShowFlyout = false;
            CurrentViewModel = new BookingsFromDayViewModel(_bookingSystemDataPersistence);
        }

        private Task<RoomListViewModel> GetBookingViewModel()
        {
            return Task.Run(() => new RoomListViewModel(_bookingSystemDataPersistence.Rooms()));
        }

        /// <summary>
        /// ShowFlyout wird invertiert
        /// </summary>
        private void ToggleFlyout()
        {
            ShowFlyout = !ShowFlyout;
        }

        #endregion

    }
}
