using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Room
{
    internal class RoomViewModel : BaseViewModel
    {
        #region Properties

        private Domain.Model.Room _room;

        /// <summary>
        /// Raum der im Raumviewmodel ist
        /// </summary>
        public Domain.Model.Room Room
        {
            get => _room;
            set => SetProperty(ref _room, value, nameof(Room));
        }

        #endregion

        #region Contructors

        /// <summary>
        /// Kontruktor für die Anzeige in der Auswahlliste
        /// </summary>
        /// <param name="room"></param>
        /// <param name="onSelect"></param>
        public RoomViewModel(Domain.Model.Room room, Action<Domain.Model.Room> onSelect)
        {
            _room = room;
            _selectRoom = onSelect;
            SelectCommand = new RelayCommand(Select);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Kommando bei Auswahl eines Raumes
        /// </summary>
        public ICommand SelectCommand { get; }

        #endregion

        #region Actions
        private void Select()
        {
            _selectRoom?.Invoke(_room);
        }

        private readonly Action<Domain.Model.Room> _selectRoom;

        #endregion
    }
}
