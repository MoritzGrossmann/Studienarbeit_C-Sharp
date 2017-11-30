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

namespace Buchungssystem.App.ViewModel.SubViewModels.BaseDataManagement
{
    internal class ChangeRoomsViewModel : BaseViewModel
    {
        #region Properties

        private IPersistBaseData _baseDataPersistence;

        private List<Room> _rooms;

        public List<Room> Rooms
        {
            get => _rooms;
            set
            {
                if (_rooms == value) return;
                _rooms = value;
                RaisePropertyChanged(nameof(Rooms));
            }
        }

        public ObservableCollection<RoomViewModel> RoomViewModels => new ObservableCollection<RoomViewModel>(_rooms.Select(r=> new RoomViewModel(_baseDataPersistence, r, SaveRoom, SelectRoom)));

        private RoomViewModel _actualRoomViewModel;

        public RoomViewModel ActualRoomViewModel
        {
            get => _actualRoomViewModel;
            set
            {
                if (_actualRoomViewModel == value) return;
                _actualRoomViewModel = value;
                RaisePropertyChanged(nameof(ActualRoomViewModel));
            }
        }

        #endregion

        #region Contructor

        public ChangeRoomsViewModel(IPersistBaseData baseDataPersistence)
        {
            _baseDataPersistence = baseDataPersistence;
            Rooms = _baseDataPersistence.Rooms();
        }

        public ChangeRoomsViewModel() : this(new TestPersitence())
        {
        }

        #endregion

        #region Actions

        private void SaveRoom(Room room)
        {
            var r = _baseDataPersistence.PersistRoom(room);
        }

        private void SelectRoom(Room room)
        {
            ActualRoomViewModel = new RoomViewModel(_baseDataPersistence, room, SaveRoom, SelectRoom);
        }

        #endregion
    }
}
