using System.Collections.Generic;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.TableView;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository.Database;

namespace Buchungssystem.App.ViewModel.RoomView
{

    /// <summary>
    /// ViewModel, welches einen Raum kapselt
    /// </summary>
    internal class RoomViewModel : BaseViewModel
    {
        #region Properties

        private Room _room;

        /// <summary>
        /// Repräsentiert den Raum in einem RoomViewModel
        /// </summary>
        public Room Room
        {
            get => _room;
            set => SetProperty(ref _room, value, nameof(Room));
        }

        /// <summary>
        /// Name des Raumes
        /// </summary>
        public string Name => Room.Name;

        /// <summary>
        /// Aktuelles ViewModel, von welchem die View angezeigt werden soll
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value, nameof(CurrentViewModel));
        }

        private BaseViewModel _currentViewModel;

        private readonly ICollection<ProductGroup> _productGroups = new BookingSystemDataPersitence().RootProductGroups();
    
        #endregion

        #region Constructor

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        /// <param name="room">Raum, welcher gekapselt wird</param>
        public RoomViewModel(Room room)
        {
            Room = room;
            CurrentViewModel = new TableListViewModel(room.Tables, SelectTable);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Setzt das CurrentViewModel auf das TableViewModel des übergebenen Tisches
        /// </summary>
        /// <param name="table">Tisch, welcher angewählt wurde</param>
        private void SelectTable(Table table)
        {
            CurrentViewModel = new TableBookViewModel(table, _productGroups, ShowTables);
        }

        /// <summary>
        /// Setzt das CurrentViewModel auf ein TableListViewModel mit alles Tischen des Raumes
        /// </summary>
        private void ShowTables()
        {
            CurrentViewModel = new TableListViewModel(Room.Tables, SelectTable);
        }

        #endregion
    }
}