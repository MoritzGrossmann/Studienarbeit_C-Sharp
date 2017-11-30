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

        private List<Table> _tables = new List<Table>();
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

        public RoomViewModel(IPersistBaseData baseDataPErsistence, Room room, Action<Room> onSave, Action<Room> onSelect)
        {
            _basedataPersistence = baseDataPErsistence;
            _onSave = onSave;
            _onSelect = onSelect;
            _room = room;

            Tables = _basedataPersistence.Tables(_room);

            SaveCommand = new RelayCommand(Save);
            SelectCommand = new RelayCommand(Select);
            EditCommand = new RelayCommand(() => Edit = !Edit);
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

        public ICommand SaveCommand { get; }

        public ICommand SelectCommand { get; }

        public ICommand EditCommand { get; }

        public ICommand ChooseTableCommand;

        public void ChooseTable()
        {
            
        }

        #endregion

        #region Actions

        private readonly Action<Room> _onSave;

        private readonly Action<Room> _onSelect;

        public void Save()
        {
            var r = new Room() { Name = Name };
            if (_room.RoomId > 0)
            {
                r.RoomId = _room.RoomId;
            }
            _onSave?.Invoke(r);
        }

        public void Select()
        {
            _onSelect?.Invoke(_room);
        }

        #endregion

        #region States

        private bool _edit = false;

        public bool Edit
        {
            get => _edit;
            set
            {
                if (_edit == value) return;
                _edit = value;
                RaisePropertyChanged(nameof(Edit));
                RaisePropertyChanged(nameof(NoEdit));
            }
        }

        public bool NoEdit => !Edit;

        #endregion
    }
}