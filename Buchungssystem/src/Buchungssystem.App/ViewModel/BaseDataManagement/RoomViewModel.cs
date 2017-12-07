using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    internal class RoomViewModel : BaseViewModel
    {
        private Room _room;

        public Room Room
        {
            get => _room;
            set => SetProperty(ref _room, value, nameof(Room));
        }

        public RoomViewModel(Room room, Action<Room> onSelect)
        {
            _room = room;
            SelectCommand = new RelayCommand(() => onSelect?.Invoke(_room));
        }

        public RoomViewModel(Room room, EventHandler<Room> onSave)
        {
            _room = room;
            SaveCommand = new RelayCommand(() => onSave(this, _room));
        }

        #region Commands

        public ICommand SaveCommand { get; }

        public ICommand SelectCommand { get; }

        #endregion
    }
}
