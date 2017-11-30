using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement;
using Buchungssystem.App.ViewModel.RoomView;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.TestRepository;
using Unity.Injection;

namespace Buchungssystem.App.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly IPersistBaseData _baseDataPersistence;

        private readonly IPersistBooking _bookingPersistence;
        public MainViewModel()
        {
            _baseDataPersistence = new TestPersitence();
            _bookingPersistence = new TestPersitence();

            _currentViewModel = new RoomListViewModel(_baseDataPersistence.Rooms());
        }


        public MainViewModel(IPersistBaseData baseDataPersistence, IPersistBooking bookingPersistence)
        {
            _baseDataPersistence = baseDataPersistence;
            _bookingPersistence = bookingPersistence;

            _currentViewModel = new RoomListViewModel(_baseDataPersistence.Rooms());

            ToBaseDataCommand = new RelayCommand(ToBaseData);
        }

        #region Propertys

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

        #endregion

        #region Actions

        private void ToBaseData()
        {
            CurrentViewModel = new BaseDataManagementViewModel(_baseDataPersistence);
        }

        #endregion

    }
}
