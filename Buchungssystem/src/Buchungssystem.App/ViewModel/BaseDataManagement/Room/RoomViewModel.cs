using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Room
{
    /// <summary>
    /// ViewModel zur Anzeige eines Raumes in der Raumliste der Stammdatenverwaltung
    /// </summary>
    internal class RoomViewModel : BaseViewModel
    {
        #region Properties

        private Domain.Model.Room _room;

        /// <summary>
        /// Repräsentiert den Raum im RooViewModel
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
        /// <param name="room">Raum</param>
        /// <param name="onSelect">Methode, die bei der Auswahl des Raumes ausgeführt werden soll</param>
        public RoomViewModel(Domain.Model.Room room, Action<Domain.Model.Room> onSelect)
        {
            _room = room;
            _selectRoom = onSelect;
            SelectCommand = new RelayCommand(Select);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Kommando zur Auswahl des Raumes
        /// </summary>
        public ICommand SelectCommand { get; }

        #endregion

        #region Actions

        /// <summary>
        /// Ruft die im Kontruktor übergebene Methode onSelect auf und übergibt den Raum
        /// </summary>
        private void Select()
        {
            _selectRoom?.Invoke(_room);
        }

        private readonly Action<Domain.Model.Room> _selectRoom;

        #endregion
    }
}
