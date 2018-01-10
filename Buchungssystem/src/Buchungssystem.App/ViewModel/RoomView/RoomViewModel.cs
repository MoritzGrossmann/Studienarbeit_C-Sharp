using System.Collections.Generic;
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
            set => SetProperty(ref _room, value, nameof(Room));
        }

        public string Name => Room.Name;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value, nameof(CurrentViewModel));
        }

        private BaseViewModel _currentViewModel;

        private readonly ICollection<ProductGroup> _productGroups = new BookingSystemDataPersitence().RootProductGroups();
    
        #endregion

        #region Constructor

        public RoomViewModel(Room room)
        {
            Room = room;
            CurrentViewModel = new TableListViewModel(room.Tables, OnTableSelect);
        }

        #endregion

        #region Actions

        private void OnTableSelect(Table table)
        {
            CurrentViewModel = new TableBookViewModel(table, _productGroups, ShowTables);
        }

        private void ShowTables()
        {
            CurrentViewModel = new TableListViewModel(Room.Tables, OnTableSelect);
        }

        #endregion
    }
}