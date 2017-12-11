using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    internal class ChangeRoomsViewModel : BaseViewModel
    {
        #region Properties

        private IPersistBookingSystemData _bookingSystemPersistence;

        public ObservableCollection<RoomViewModel> RoomViewModels { get; set; }

        private RoomViewModel _actualRoomViewModel;

        public RoomViewModel ActualRoomViewModel
        {
            get => _actualRoomViewModel;
            set => SetProperty(ref _actualRoomViewModel, value, nameof(ActualRoomViewModel));
        }

        #endregion

        #region Constructor

        public ChangeRoomsViewModel(ICollection<Room> rooms, IPersistBookingSystemData bookingSystemPersistence)
        {
            _bookingSystemPersistence = bookingSystemPersistence;
            RoomViewModels = new ObservableCollection<RoomViewModel>(rooms.Select(r => new RoomViewModel(r, SelectRoom)));
            AddRoomCommand = new RelayCommand(AddRoom);
            ActualRoomViewModel = RoomViewModels.FirstOrDefault();
        }

        #endregion

        #region Actions

        private void SelectRoom(Room room)
        {
            ActualRoomViewModel = new RoomViewModel(room, SaveRoom);
            RaisePropertyChanged(nameof(ActualRoomViewModel));
        }

        private void SaveRoom(object sender, Room room)
        {
            try
            {
                var r = room.Persist();
                RoomViewModels.Add(new RoomViewModel(r, SelectRoom));
            }
            catch (ModelExistException modelExistException)
            {
                // TODO
            }
        }

        private void AddRoom()
        {
            ActualRoomViewModel = new RoomViewModel(new Room() {Name = "", Persistence = _bookingSystemPersistence}, SaveRoom);
        }

        #endregion

        #region Commands

        public ICommand AddRoomCommand { get; }

        #endregion

    }
}
