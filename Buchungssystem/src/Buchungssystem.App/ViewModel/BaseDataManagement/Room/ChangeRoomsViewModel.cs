using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Room
{
    internal class ChangeRoomsViewModel : BaseViewModel
    {
        #region Properties

        private readonly IPersistBookingSystemData _bookingSystemPersistence;

        public ObservableCollection<RoomViewModel> RoomViewModels { get; set; }

        private BaseViewModel _actualRoomViewModel;

        public BaseViewModel ActualRoomViewModel
        {
            get => _actualRoomViewModel;
            set => SetProperty(ref _actualRoomViewModel, value, nameof(ActualRoomViewModel));
        }

        #endregion

        #region Constructor

        public ChangeRoomsViewModel(IPersistBookingSystemData bookingSystemPersistence)
        {
            _bookingSystemPersistence = bookingSystemPersistence;
            RoomViewModels = new ObservableCollection<RoomViewModel>(_bookingSystemPersistence.Rooms().Select(r => new RoomViewModel(r, SelectRoom)));
            AddRoomCommand = new RelayCommand(AddRoom);
            ActualRoomViewModel = RoomViewModels.Any() ? new CreateRoomViewModel(SaveRoom, Delete, _bookingSystemPersistence, RoomViewModels.FirstOrDefault().Room) : null;
        }

        #endregion

        #region Actions

        private void SelectRoom(Domain.Model.Room room)
        {
            ActualRoomViewModel = new CreateRoomViewModel(SaveRoom, Delete, _bookingSystemPersistence, room);
        }

        private void SaveRoom(object sender, Domain.Model.Room room)
        {

            try
            {

                int oldId = room.Id;

            var r = room.Persist();
            if (r.Id != oldId)
            {
                RoomViewModels.Add(new RoomViewModel(r, SelectRoom));
                ActualRoomViewModel = new CreateRoomViewModel(SaveRoom, Delete, _bookingSystemPersistence, r);
            }
            else
            {
                RoomViewModels.FirstOrDefault(rvm => rvm.Room.Id == room.Id).Room = r;
            }
            }
            catch (ModelExistException ex)
            {
                throw ex;
            }
        }

        private void Delete(object sender, Domain.Model.Room room)
        {
            var roomViewModel = RoomViewModels.FirstOrDefault(r => r.Room.Id == room.Id);
            RoomViewModels.Remove(roomViewModel);
            ActualRoomViewModel = RoomViewModels.Any() ? new CreateRoomViewModel(SaveRoom, Delete, _bookingSystemPersistence, RoomViewModels.FirstOrDefault().Room) : new CreateRoomViewModel(SaveRoom, _bookingSystemPersistence);
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
