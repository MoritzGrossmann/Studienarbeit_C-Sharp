using System;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.TableView;
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

        public string Name => Room.Name;

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; }
        }

        private BaseViewModel _currentViewModel;
    
        #endregion

        #region Contructor

        public RoomViewModel(Room room, Action<RoomViewModel> onSelect)
        {
            Room = room;
            CurrentViewModel = new TableListViewModel(room.Tables, SelectTable);
            RaisePropertyChanged(nameof(Room));
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

        private void SelectTable(Table table)
        {
            CurrentViewModel = new TableBookViewModel(table, ShowTables);
            RaisePropertyChanged(nameof(CurrentViewModel));
        }

        private void ShowTables()
        {
            CurrentViewModel = new TableListViewModel(Room.Tables, SelectTable);
            RaisePropertyChanged(nameof(CurrentViewModel));
        }

        #endregion
    }
}