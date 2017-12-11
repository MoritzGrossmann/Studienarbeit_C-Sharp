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

        private bool _edit;

        public bool Edit
        {
            get => _edit;
            set => SetProperty(ref _edit, value, nameof(Edit));
        }

        public bool NoEdit => !Edit;

        public RoomViewModel(Room room, Action<Room> onSelect)
        {
            _room = room;
            _selectRoom = onSelect;
            SelectCommand = new RelayCommand(Select);
        }

        public RoomViewModel(Room room, EventHandler<Room> onSave)
        {
            _room = room;
            SaveCommand = new RelayCommand(() => onSave(this, _room));
            EditCommand = new RelayCommand(ToogleEdit);
        }

        private void ToogleEdit()
        {
            Edit = !Edit;
            RaisePropertyChanged(nameof(NoEdit));
        }



        #region Commands

        public ICommand SaveCommand { get; }

        public ICommand SelectCommand { get; }

        public ICommand EditCommand { get; }
        #endregion

        private void Select()
        {
            _selectRoom?.Invoke(_room);
        }

        private Action<Room> _selectRoom;
    }
}
