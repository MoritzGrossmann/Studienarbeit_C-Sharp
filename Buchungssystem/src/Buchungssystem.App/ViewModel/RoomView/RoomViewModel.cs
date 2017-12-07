using System;
using System.Collections.Generic;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.TableView;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository.Database;

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

        private readonly ICollection<ProductGroup> _productGroups = new BookingSystemDataPersitence().ProductGroups();
    
        #endregion

        #region Constructor

        public RoomViewModel(Room room, Action<RoomViewModel> onSelect)
        {
            Room = room;
            CurrentViewModel = new TableListViewModel(room.Tables, OnTableSelect);
            RaisePropertyChanged(nameof(Room));
            _select = onSelect;
        }

        #endregion

        #region Actions

        private readonly Action<RoomViewModel> _select;

        private void OnTableSelect(object sender, Table table)
        {
            CurrentViewModel = new TableBookViewModel(table, _productGroups, ShowTables);
            RaisePropertyChanged(nameof(CurrentViewModel));
        }

        private void ShowTables()
        {
            CurrentViewModel = new TableListViewModel(Room.Tables, OnTableSelect);
            RaisePropertyChanged(nameof(CurrentViewModel));
        }

        #endregion
    }
}