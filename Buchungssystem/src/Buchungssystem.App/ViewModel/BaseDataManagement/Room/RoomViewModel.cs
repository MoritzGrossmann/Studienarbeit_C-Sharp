using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Room
{
    internal class RoomViewModel : BaseViewModel
    {
        private Domain.Model.Room _room;

        /// <summary>
        /// Raum der im Raumviewmodel ist
        /// </summary>
        public Domain.Model.Room Room
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
        public RoomViewModel(Domain.Model.Room room, Action<Domain.Model.Room> onSelect)
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
        public RoomViewModel(Domain.Model.Room room, EventHandler<Domain.Model.Room> onSave)
        {
            _room = room;
            _onSave = onSave;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToogleEdit);
            TableViewModels = new ObservableCollection<TableViewModel>(room.Tables.Select(t => new TableViewModel(t, DeleteTable)));
        }

        private void ToogleEdit()
        {
            Edit = !Edit;
            RaisePropertyChanged(nameof(NoEdit));
        }

        private readonly EventHandler<Domain.Model.Room> _onSave;

        /// <summary>
        /// Lößt den Eventhandler _onSave aus
        /// Speichert den Raum in der Datenbank
        /// Ändert die Editieransicht auf "Nicht editieren"
        /// </summary>
        private void Save()
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

        /// <summary>
        /// erlaubt das Editieren des Raumes
        /// </summary>
        public ICommand EditCommand { get; }
        #endregion

        /// <summary>
        /// Event wenn Raum änderungen am Raum gespeichert wird
        /// </summary>


        private void Select()
        {
            _selectRoom?.Invoke(_room);
        }

        private readonly Action<Domain.Model.Room> _selectRoom;
    }
}
