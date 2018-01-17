using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement;
using Buchungssystem.App.ViewModel.Loading;
using Buchungssystem.App.ViewModel.RoomView;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;


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

        #endregion

        #region Actions

        /// <summary>
        /// Setzt ShowFlyout auf False
        /// Setzut das CurrentViewModel auf ein neues BaseDataManagementViewModel
        /// </summary>
        private void ToBaseData()
        {
            ShowFlyout = false;

            CurrentViewModel = new LoadingViewModel();
            TaskAwaiter<BaseDataManagementViewModel> awaiter = GetBaseDataManagementViewModel().GetAwaiter();

            awaiter.OnCompleted(() => CurrentViewModel = awaiter.GetResult());

        }

        private Task<BaseDataManagementViewModel> GetBaseDataManagementViewModel()
        {
            return Task.Run(() => new BaseDataManagementViewModel(_bookingSystemDataPersistence));
        }

        /// <summary>
        /// Setzt ShowFlyout auf False
        /// Setzut das CurrentViewModel auf ein neues RoomListViewModel
        /// </summary>
        private void ToBooking()
        {
            ShowFlyout = false;

            CurrentViewModel = new LoadingViewModel();
            TaskAwaiter<RoomListViewModel> awaiter = GetBookingViewModel().GetAwaiter();

            awaiter.OnCompleted(() => CurrentViewModel = awaiter.GetResult());
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
