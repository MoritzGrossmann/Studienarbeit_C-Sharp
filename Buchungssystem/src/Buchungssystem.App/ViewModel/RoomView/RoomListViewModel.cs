using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.RoomView
{
    /// <summary>
    /// Repräsentiert eine Liste von RoomViewModel
    /// </summary>
    internal class RoomListViewModel : BaseViewModel
    {
        private RoomViewModel _selectedRoom;

        /// <summary>
        /// Repräsentiert das angewählte RoomViewModel
        /// </summary>
        public RoomViewModel SelectedRoom
        {
            get => _selectedRoom;
            set => SetProperty(ref _selectedRoom, value, nameof(SelectedRoom));
        }

        private ObservableCollection<RoomViewModel> _roomViewModels;

        /// <summary>
        /// List von RommViewModel aller existenten Räume
        /// </summary>
        public ObservableCollection<RoomViewModel> RoomViewModels
        {
            get => _roomViewModels;
            set => SetProperty(ref _roomViewModels, value, nameof(RoomViewModels));
        }

        public RoomListViewModel(ICollection<Room> rooms) 
        {
           RoomViewModels = new ObservableCollection<RoomViewModel>(rooms.Select(r => new RoomViewModel(r)));
        }
    }
}
