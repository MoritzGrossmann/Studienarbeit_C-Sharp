using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.RoomView;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    internal class ChangeRoomsViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<RoomViewModel> RoomViewModels { get; set; }

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

        public ChangeRoomsViewModel(ICollection<Room> rooms)
        {
            RoomViewModels = new ObservableCollection<RoomViewModel>(rooms.Select(r => new RoomViewModel(r, SelectRoom)));
            ActualRoomViewModel = RoomViewModels.FirstOrDefault();
        }

        #endregion

        #region Actions

        private void SelectRoom(RoomViewModel roomViewModel)
        {
            ActualRoomViewModel = roomViewModel;
        }

        #endregion

    }
}
