﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    internal class RoomViewModel : BaseViewModel
    {
        private Room _room;

        /// <summary>
        /// Raum der im Raumviewmodel ist
        /// </summary>
        public Room Room
        {
            get => _room;
            set => SetProperty(ref _room, value, nameof(Room));
        }

        private ObservableCollection<TableViewModel> _tableViewModels;

        /// <summary>
        /// Tische in einem Raum
        /// </summary>
        public ObservableCollection<TableViewModel> TableViewModels
        {
            get => _tableViewModels;
            set => SetProperty(ref _tableViewModels, value, nameof(TableViewModels));
        }

        private bool _edit;

        /// <summary>
        /// Zeigt an, ob der Raumname gerade Editiert wird
        /// </summary>
        public bool Edit
        {
            get => _edit;
            set => SetProperty(ref _edit, value, nameof(Edit));
        }

        /// <summary>
        /// Zeigt an, ib der Raumname gerade nicht editiert wird
        /// </summary>
        public bool NoEdit => !Edit;

        /// <summary>
        /// Kontruktor für die Anzeige in der Auswahlliste
        /// </summary>
        /// <param name="room"></param>
        /// <param name="onSelect"></param>
        public RoomViewModel(Room room, Action<Room> onSelect)
        {
            _room = room;
            _selectRoom = onSelect;
            SelectCommand = new RelayCommand(Select);
        }

        /// <summary>
        /// Konstruktor für das Editieren
        /// </summary>
        /// <param name="room"></param>
        /// <param name="onSave"></param>
        public RoomViewModel(Room room, EventHandler<Room> onSave)
        {
            _room = room;
            _onSave = onSave;

            SaveCommand = new RelayCommand(SaveRoom);
            EditCommand = new RelayCommand(ToogleEdit);
            TableViewModels = new ObservableCollection<TableViewModel>(room.Tables.Select(t => new TableViewModel(t, DeleteTable)));
        }

        private void ToogleEdit()
        {
            Edit = !Edit;
            RaisePropertyChanged(nameof(NoEdit));
        }

        /// <summary>
        /// Lößt den Eventhandler _onSave aus
        /// Speichert den Raum in der Datenbank
        /// Ändert die Editieransicht auf "Nicht editieren"
        /// </summary>
        private void SaveRoom()
        {
            _onSave?.Invoke(this,_room);
            ToogleEdit();
        }

        private void DeleteTable(object sender, Table table)
        {
            // TODO Delete Table
            TableViewModels.Remove((TableViewModel) sender);
        }

        #region Commands

        /// <summary>
        /// Kommando beim Speichern eines Raumes
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Kommando bei Auswahl eines Raumes
        /// </summary>
        public ICommand SelectCommand { get; }


        public ICommand EditCommand { get; }
        #endregion

        /// <summary>
        /// Event wenn Raum änderungen am Raum gespeichert wird
        /// </summary>
        private readonly EventHandler<Room> _onSave;

        private void Select()
        {
            _selectRoom?.Invoke(_room);
        }

        private readonly Action<Room> _selectRoom;
    }
}
