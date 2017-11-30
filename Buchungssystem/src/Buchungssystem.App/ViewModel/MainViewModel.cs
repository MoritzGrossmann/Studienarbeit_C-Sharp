using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.SubViewModels.BaseDataManagement;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.TestRepository;
using Unity.Injection;

namespace Buchungssystem.App.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            _baseDataPersistence = new TestPersitence();
            _bookingPersistence = new TestPersitence();

            _currentViewModel = new RoomsViewModel();
        }


        public MainViewModel(IPersistBaseData baseDataPersistence, IPersistBooking bookingPersistence)
        {
            _baseDataPersistence = baseDataPersistence;
            _bookingPersistence = bookingPersistence;

            _currentViewModel = new RoomsViewModel(_baseDataPersistence, _bookingPersistence, TableSelected);

            ToBaseDataCommand = new RelayCommand(ToBaseData);
        }

        void TableSelected(Table table)
        {
            CurrentViewModel = new TableBookViewModel(_baseDataPersistence, _bookingPersistence, ShowRoomsView, table);
        }

        private readonly IPersistBaseData _baseDataPersistence;

        private readonly IPersistBooking _bookingPersistence;

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

        private void ShowRoomsView()
        {
            CurrentViewModel = new RoomsViewModel(_baseDataPersistence, _bookingPersistence, TableSelected);
        }

        private void ToBaseData()
        {
            CurrentViewModel = new BaseDataManagementViewModel(_baseDataPersistence);
        }

        #endregion

    }
}
