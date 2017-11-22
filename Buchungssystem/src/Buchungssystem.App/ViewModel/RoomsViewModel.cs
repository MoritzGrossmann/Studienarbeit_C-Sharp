using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel
{
    internal class RoomsViewModel : BaseViewModel
    {
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

        public ObservableCollection<RoomViewModel> RoomViewModels => new ObservableCollection<RoomViewModel>(_rooms.Select(room => new RoomViewModel(room, _onTableSelected)));

        public RoomsViewModel() : this(new List<Room>() { 
            new Room() {
            Name = "Test",
            RoomId = 1,
            Tables = new List<Table>(new[]
            {
                new Table {Bookings = new List<Booking>(), Name = "Tisch1", Places = 4, RoomId = 1}
            })
            }  
        }, null)
        {
            
        }

        public RoomsViewModel(ICollection<Room> rooms, Action<Table> onTableSelected)
        {
            _onTableSelected = onTableSelected;
            _rooms = rooms;
        }

        private Action<Table> _onTableSelected;
    }
}
