using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement;
using Buchungssystem.App.ViewModel.Bookings;
using Buchungssystem.App.ViewModel.Loading;
using Buchungssystem.App.ViewModel.RoomView;
using Buchungssystem.Domain.Database;
using MahApps.Metro.Controls.Dialogs;


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

            TaskAwaiter<RoomListViewModel> awaiter = Task.Run(() =>
            {
                return new RoomListViewModel(_bookingSystemDataPersistence.Rooms());
            }).GetAwaiter();

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
        /// Zeit an, ob das Hamburger-Menu ausgeklappt ist
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
        /// Setzt das CurrentViewModel auf ein neues BaseDataManagementViewModel
        /// </summary>
        private async void ToBaseData()
        {
            try
            {

                ShowFlyout = false;

                Loading = true;

                CurrentViewModel = new LoadingViewModel();

                var viewModel = await GetBaseDataManagementViewModel();

                CurrentViewModel = viewModel;
            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Laden der daten aus der Datenbank",
                    $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                Loading = false;
            }
        }


        /// <summary>
        /// Setzt ShowFlyout auf False
        /// Setzt das CurrentViewModel auf ein neues RoomListViewModel
        /// </summary>
        private async void ToBooking()
        {
            try
            {

                ShowFlyout = false;

                Loading = true;

                CurrentViewModel = new LoadingViewModel();

                var viewModel = await GetBookingViewModel();

                CurrentViewModel = viewModel;

                Loading = false;
            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Laden der daten aus der Datenbank",
                    $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                Loading = false;
            }
        }

        /// <summary>
        /// Setzt ShowFlyout auf False
        /// Setzt das CurrentViewModel auf ein neues BookingsFromDayViewModel
        /// </summary>
        private async void ToOverview()
        {
            try
            {
                ShowFlyout = false;

                Loading = true;

                CurrentViewModel = new LoadingViewModel();

                var viewModel = await GetBookingsFromDayViewModel();

                CurrentViewModel = viewModel;

                Loading = false;

            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Laden der Daten aus der Datenbank",
                    $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                Loading = false;
            }
        }

        /// <summary>
        /// Erstellt asynchron neues BookingsFromDayViewModel
        /// </summary>
        /// <returns></returns>
        private async Task<BookingsFromDayViewModel> GetBookingsFromDayViewModel()
        {
            return await Task.Run(() =>
                new BookingsFromDayViewModel(_bookingSystemDataPersistence));
        }

        /// <summary>
        /// Erstellt asynchron neues RoomListViewModel
        /// </summary>
        /// <returns></returns>
        private async Task<RoomListViewModel> GetBookingViewModel()
        {
            return await Task.Run(() => new RoomListViewModel(_bookingSystemDataPersistence.Rooms()));
        }

        /// <summary>
        /// Erstellt asynchron neues BaseDataManagementViewModel
        /// </summary>
        /// <returns></returns>
        private async Task<BaseDataManagementViewModel> GetBaseDataManagementViewModel()
        {
            return await Task.Run(() => new BaseDataManagementViewModel(_bookingSystemDataPersistence));
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
