using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;

namespace Buchungssystem.App.ViewModel
{

    /// <summary>
    /// View Model for each Room
    /// </summary>
    internal class RoomViewModel : BaseViewModel
    {

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

        private ObservableCollection<TableViewModel> _tables;
        public ObservableCollection<TableViewModel> Tables
        {
            get => _tables;
            set
            {
                if (_tables.Equals(value)) return;
                _tables = value;
                RaisePropertyChanged(nameof(Tables));
            }
        }

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

        public RoomViewModel(Room room, Action<Table> onTableSelected)
        {
            ChooseTableCommand = new RelayCommand(ChooseTable);
            SelectCommand = new RelayCommand(Select);
            _room = room;
            _onTableSelected = onTableSelected;
            _tables = new ObservableCollection<TableViewModel>(
                room.Tables.Select(table => new TableViewModel(table, onTableSelected))
            );
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