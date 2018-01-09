using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel.RoomView
{
    internal class RoomListViewModel : BaseViewModel
    {
        private RoomViewModel _selectedRoom;

        public RoomViewModel SelectedRoom
        {
            get => _selectedRoom;
            set => SetProperty(ref _selectedRoom, value, nameof(SelectedRoom));
        }

        private ObservableCollection<RoomViewModel> _roomViewModels;

        public ObservableCollection<RoomViewModel> RoomViewModels
        {
            get => _roomViewModels;
            set => SetProperty(ref _roomViewModels, value, nameof(RoomViewModels));
        }

        public RoomListViewModel(ICollection<Room> rooms) 
        {
           RoomViewModels = new ObservableCollection<RoomViewModel>(rooms.Select(r => new RoomViewModel(r)));
        }

        public RoomListViewModel() : this(new TestPersitence().Rooms())
        {
            
        }
    }
}
