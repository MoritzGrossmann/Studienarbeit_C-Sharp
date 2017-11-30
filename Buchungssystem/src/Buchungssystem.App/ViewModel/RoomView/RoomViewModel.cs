using System;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.RoomView
{

    /// <summary>
    /// View Model for each Room
    /// </summary>
    internal class RoomViewModel : BaseViewModel
    {
        #region Properties

        private Room _room;

        public Room Room
        {
            get => _room;
            set => _room = value;
        }
    
        #endregion

        #region Contructor

        public RoomViewModel(Room room, Action<RoomViewModel> onSelect)
        {
            Room = room;
            _select = onSelect;
        }

        #endregion

        #region Commands

        public void ChooseTable()
        {
            
        }

        #endregion

        #region Actions

        private readonly Action<RoomViewModel> _select;

        #endregion
    }
}