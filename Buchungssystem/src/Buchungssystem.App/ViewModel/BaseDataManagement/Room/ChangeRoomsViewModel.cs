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

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit RoomViewModels.Any() ausgeschlossen
            ActualRoomViewModel = RoomViewModels.Any() ? new EditRoomViewModel(Save, Delete, _bookingSystemPersistence, RoomViewModels.FirstOrDefault().Room) : null;
        }

        #endregion

        #region Actions

        private void SelectRoom(Domain.Model.Room room)
        {
            ActualRoomViewModel = new EditRoomViewModel(Save, Delete, _bookingSystemPersistence, room);
        }

        private void Save(Domain.Model.Room room)
        {
            var roomViewModel = RoomViewModels.FirstOrDefault(r => r.Room.Id == room.Id);

            if (roomViewModel != null)
                roomViewModel.Room = room;
            else
                RoomViewModels.Add(new RoomViewModel(room, SelectRoom));

            ActualRoomViewModel = new EditRoomViewModel(Save, Delete, _bookingSystemPersistence, room);
        }

        private void Delete(Domain.Model.Room room)
        {
            var roomViewModel = RoomViewModels.FirstOrDefault(r => r.Room.Id == room.Id);
            RoomViewModels.Remove(roomViewModel);

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit RoomViewModels.Any() ausgeschlossen
            ActualRoomViewModel = RoomViewModels.Any() ? new EditRoomViewModel(Save, Delete, _bookingSystemPersistence, RoomViewModels.FirstOrDefault().Room) : new EditRoomViewModel(Save, _bookingSystemPersistence);
        }

        private void AddRoom()
        {
            ActualRoomViewModel = new EditRoomViewModel(Save, _bookingSystemPersistence);
        }

        #endregion

        #region Commands

        public ICommand AddRoomCommand { get; }

        #endregion

    }
}
