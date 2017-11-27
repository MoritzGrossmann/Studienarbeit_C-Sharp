using Buchungssystem.App.ViewModel.Base;
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

            _currentViewModel = new RoomViewModel();
        }


        public MainViewModel(IPersistBaseData baseDataPersistence, IPersistBooking bookingPersistence)
        {
            _baseDataPersistence = baseDataPersistence;
            _bookingPersistence = bookingPersistence;

            _currentViewModel = baseDataPersistence == null
                ? new RoomsViewModel()
                : new RoomsViewModel(_baseDataPersistence, _bookingPersistence, TableSelected);
        }

        void TableSelected(Table table)
        {
            _currentViewModel = new TableBookViewModel(_baseDataPersistence, _bookingPersistence, table);
            RaisePropertyChanged(nameof(CurrentViewModel));
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

        #endregion

    }
}
