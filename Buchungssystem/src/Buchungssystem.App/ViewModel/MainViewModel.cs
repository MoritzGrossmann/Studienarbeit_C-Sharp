using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Unity.Injection;

namespace Buchungssystem.App.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel() : this(null, null)
        {
        }


        public MainViewModel(IPersistBaseData baseDataPersistence, IPersistBooking bookingPersistence)
        {
            _baseDataPersistence = baseDataPersistence;
            _bookingPersistence = bookingPersistence;

            _currentViewModel = baseDataPersistence == null
                ? new RoomsViewModel()
                : new RoomsViewModel(_baseDataPersistence.Rooms(), TableSelected);
        }

        void TableSelected(Table table)
        {
            _currentViewModel = new TableBookViewModel(table, _baseDataPersistence, _bookingPersistence);
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
