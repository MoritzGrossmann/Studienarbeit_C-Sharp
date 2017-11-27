using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel
{
    internal class RoomsViewModel : BaseViewModel
    {
        private readonly IPersistBaseData _baseDataPersitence;

        private readonly IPersistBooking _bookingPersistence;

        private ICollection<Room> _rooms;

        public ICollection<Room> Rooms
        {
            get => _rooms;
            set
            {
                if (Rooms.Equals(value)) return;
                _rooms = value;
                RaisePropertyChanged(nameof(Rooms));
                RaisePropertyChanged(nameof(RoomViewModels));
            }
        }

        public ObservableCollection<RoomViewModel> RoomViewModels => new ObservableCollection<RoomViewModel>(_rooms.Select(room => new RoomViewModel(_baseDataPersitence, _bookingPersistence, room, _onTableSelected)));

        public RoomsViewModel() 
        {
            _baseDataPersitence = new TestPersitence();
            _bookingPersistence = new TestPersitence();
            _onTableSelected = null;
            _rooms = _baseDataPersitence.Rooms();
        }

        public RoomsViewModel(IPersistBaseData baseDataPersitence, IPersistBooking bookingPersistence, Action<Table> onTableSelected)
        {
            _baseDataPersitence = baseDataPersitence;
            _bookingPersistence = bookingPersistence;
            _onTableSelected = onTableSelected;
            _rooms = _baseDataPersitence.Rooms();
        }

        private Action<Table> _onTableSelected;
    }
}
