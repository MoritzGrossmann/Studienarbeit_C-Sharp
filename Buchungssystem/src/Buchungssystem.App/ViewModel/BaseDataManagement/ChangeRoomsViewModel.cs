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

        private BaseViewModel _actualRoomViewModel;

        public BaseViewModel ActualRoomViewModel
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
            //ActualRoomViewModel = new CreateRoomViewModel(SaveRoom, _bookingSystemPersistence);
            ActualRoomViewModel = new RoomViewModel(RoomViewModels.FirstOrDefault().Room, SaveRoom);
        }

        #endregion

        #region Actions

        private void SelectRoom(Room room)
        {
            ActualRoomViewModel = new RoomViewModel(room, SaveRoom);
            //RaisePropertyChanged(nameof(ActualRoomViewModel));
        }

        private void SaveRoom(object sender, Room room)
        {

            int oldId = room.Id;

            var r = room.Persist();
            if (r.Id != oldId)
            {
                RoomViewModels.Add(new RoomViewModel(r, SelectRoom));
                ActualRoomViewModel = new RoomViewModel(r, SaveRoom);
                Console.WriteLine($"INFO:\tRaum {room.Name} angelegt");
            }
            else
            {
                RoomViewModels.FirstOrDefault(rvm => rvm.Room.Id == room.Id).Room = r;
            }
        }

        private void AddRoom()
        {
            ActualRoomViewModel = new CreateRoomViewModel(SaveRoom, _bookingSystemPersistence);
        }

        #endregion

        #region Commands

        public ICommand AddRoomCommand { get; }

        #endregion

    }
}
