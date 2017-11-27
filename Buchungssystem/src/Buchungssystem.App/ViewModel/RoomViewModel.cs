using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel
{

    /// <summary>
    /// View Model for each Room
    /// </summary>
    internal class RoomViewModel : BaseViewModel
    {
        private readonly IPersistBaseData _basedataPersistence;

        private readonly IPersistBooking _bookingPersistence;


        #region Properties

        private Room _room;

        private readonly Action<Table> _onTableSelected;

        public Room Room
        {
            get => _room;

            set
            {
                if (_room.Equals(value)) return;
                _room = value;
                RaisePropertyChanged(nameof(Room));
            }
        }

        /// <summary>
        /// Repräsentiert die Tables, die in einem Room stehen
        /// </summary>

        private List<Table> _tables;
        public List<Table> Tables
        {
            get => _tables;
            set
            {
                if (_tables.Equals(value)) return;
                _tables = value;
                RaisePropertyChanged(nameof(Tables));
            }
        }

        public ObservableCollection<TableViewModel> TableViewModels => new ObservableCollection<TableViewModel>(
            _tables.Select(t => new TableViewModel(_basedataPersistence, _bookingPersistence, t, _onTableSelected)));

        private TableViewModel _selectedTable;

        public TableViewModel SelectedTable
        {
            get => _selectedTable;
            set
            {
                if (_selectedTable.Equals(value)) return;
                _selectedTable = value;
                RaisePropertyChanged(nameof(SelectedTable));
            }
        }

        /// <summary>
        /// Name des Raumes
        /// </summary>
        
        public string Name
        {
            get => _room.Name;
            set
            {
                if (Equals(_room.Name, value)) return;
                _room.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public bool CanSelect
        {
            get => Tables.Any();
        }

        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set
            {
                if (_selected.Equals(value)) return;
                _selected = value;
                RaisePropertyChanged(nameof(Selected));
                if (value) Select();
            }
        }

        #endregion

        #region Contructor

        public RoomViewModel(IPersistBaseData basedataPersistence, IPersistBooking bookingPersistence, Room room, Action<Table> onTableSelected)
        {
            _basedataPersistence = basedataPersistence;
            _bookingPersistence = bookingPersistence;

            ChooseTableCommand = new RelayCommand(ChooseTable);
            SelectCommand = new RelayCommand(Select);
            _room = room;
            _onTableSelected = onTableSelected;
            _tables = _basedataPersistence.Tables(_room);
        }

        public RoomViewModel()
        {
            _basedataPersistence = new TestPersitence();
            _bookingPersistence = new TestPersitence();

            _room = _basedataPersistence.Rooms().FirstOrDefault();
            _onTableSelected = null;
        }

        #endregion

        #region Commands

        public ICommand SelectCommand;

        public void Select()
        {
            
        }

        public ICommand ChooseTableCommand;

        public void ChooseTable()
        {
            
        }

        #endregion
    }
}