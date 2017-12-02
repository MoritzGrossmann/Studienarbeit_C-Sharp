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

namespace Buchungssystem.App.ViewModel.RoomView
{
    internal class RoomListViewModel : BaseViewModel
    {
        private RoomViewModel _selectedRoom;

        public RoomViewModel SelectedRoom
        {
            get => _selectedRoom;
            set => _selectedRoom = value;
        }


        private ObservableCollection<RoomViewModel> _roomViewModels;
        public ObservableCollection<RoomViewModel> RoomViewModels { get => _roomViewModels; set => _roomViewModels = value; }

        public RoomListViewModel(ICollection<Room> rooms) 
        {
           RoomViewModels = new ObservableCollection<RoomViewModel>(rooms.Select(r => new RoomViewModel(r, SelectRoom)));
        }

        public RoomListViewModel() : this(new TestPersitence().Rooms())
        {
            
        }

        private void SelectRoom(RoomViewModel roomViewModel)
        {
            SelectedRoom = roomViewModel;
        }
    }
}
