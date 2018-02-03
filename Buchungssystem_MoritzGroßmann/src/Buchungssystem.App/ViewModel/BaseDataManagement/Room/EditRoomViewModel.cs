using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement.Table;
using Buchungssystem.Domain.Database;
using MahApps.Metro.Controls.Dialogs;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Room
{
    /// <summary>
    /// ViewModel zum Erstellen oder Editieren von Räumen
    /// </summary>
    internal class EditRoomViewModel : EditViewModel
    {
        /// <summary>
        /// Repräsentiert die Id des bearbeitenten Raumes
        /// </summary>
        public int Id { get; }

        private ICollection<Domain.Model.Table> _tables;

        private readonly Domain.Model.Room _room;

        #region Properties

        private ObservableCollection<TableViewModel> _tableViewModels;

        /// <summary>
        /// Repräsentiert die Tische in einem Raum
        /// </summary>
        public ObservableCollection<TableViewModel> TableViewModels
        {
            get => _tableViewModels;
            set => SetProperty(ref _tableViewModels, value, nameof(TableViewModels));
        }

        private BaseViewModel _editTableViewModel;

        /// <summary>
        /// Repräsentiert den zum Bearbeiten angezeigten Tisch in einem Raum
        /// </summary>
        public BaseViewModel EditTableViewModel
        {
            get => _editTableViewModel;
            set => SetProperty(ref _editTableViewModel, value, nameof(EditTableViewModel));
        }

        private string _query = String.Empty;

        /// <summary>
        /// Repräsentiert den String in der Warensuche
        /// </summary>
        public string Query
        {
            get => _query;
            set
            {
                if (value.Trim().Equals(String.Empty))
                {
                    TableViewModels =
                        new ObservableCollection<TableViewModel>(
                            _tables.Select(p => new TableViewModel(p, SelectTable)));
                }
                else
                {
                    TableViewModels =
                        new ObservableCollection<TableViewModel>(
                            _tables.Where(t => t.Name.ToLower().Contains(value.ToLower())).Select(t => new TableViewModel(t, SelectTable)));
                }
                SetProperty(ref _query, value, nameof(Query));

            }
        }

        #endregion

        #region Contructor

        /// <summary>
        /// Konstruktor zum Erstellen eines neuen Raumes
        /// </summary>
        /// <param name="onSave">Aktion, welche beim Speichern eines Raumes aufgerufen wird</param>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
        public EditRoomViewModel(Action<Domain.Model.Room> onSave, IPersistBookingSystemData bookingSystemPersistence)
        {
            HeaderText = "Neuen Raum anlegen";
            Name = String.Empty;
            Edit = true;
            BookingSystemPersistence = bookingSystemPersistence;
            _onSave = onSave;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);
            AddTableCommand = new RelayCommand(AddTable);

            _tables = new List<Domain.Model.Table>();

            TableViewModels = new ObservableCollection<TableViewModel>();
            EditTableViewModel = null;
        }

        /// <summary>
        /// Kontruktor zum Bearbeiten eines neuen Raumes
        /// </summary>
        /// <param name="onSave">Aktion, welche beim Speichern eines Raumes aufgerufen wird</param>
        /// <param name="onDelete">Aktion, welche beim Löschen eines Raumes aufgerufen wird</param>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
        /// <param name="room">Raum, welcher bearbeitet werden soll</param>
        public EditRoomViewModel(Action<Domain.Model.Room> onSave, Action<Domain.Model.Room> onDelete,
            IPersistBookingSystemData bookingSystemPersistence, Domain.Model.Room room)
        {
            HeaderText = $"{room.Name} bearbeiten";
            Id = room.Id;
            _room = room;
            Name = room.Name;

            BookingSystemPersistence = bookingSystemPersistence;

            _onSave = onSave;
            _onDelete = onDelete;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);
            AddTableCommand = new RelayCommand(AddTable);

            _tables = room.Tables?.OrderBy(t => t.Name).ToList() ?? new List<Domain.Model.Table>();

            TableViewModels = new ObservableCollection<TableViewModel>(_tables.Select(t => new TableViewModel(t, SelectTable)));

            EditTableViewModel = _tables.Any() ? new EditTableViewModel(SaveTable, _room, DeleteTable, _tables.FirstOrDefault(), BookingSystemPersistence) : new EditTableViewModel(SaveTable, _room, BookingSystemPersistence);
        }

        #endregion

        #region Commands

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommando zum Hinzufügen eines neuen Tisches
        /// </summary>

        public ICommand AddTableCommand { get; }

        #endregion

        #region Actions

        /// <summary>
        /// Speichert den bearbeiteten Raum in der Datenbank
        /// Ruft die im Konstruktor übergebene Methode onSave auf
        /// </summary>
        private async void Save()
        {
            try
            {
                var room = new Domain.Model.Room() {Id = Id, Name = Name, Persistence = BookingSystemPersistence};

                ShowProgressbar = true;

                var r = await room.Persist();

                ShowProgressbar = false;

                _onSave.Invoke(r);
            }
            catch (ModelExistException ex)
            {
                AddError(nameof(Name),ex.Message);
                RaisePropertyChanged(nameof(HasErrors));
            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Speichern des Raumes", $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                ShowProgressbar = false;
            }
        }

        /// <summary>
        /// Löscht einen Raum 
        /// Ruft die im Konstruktor übergebene Methode onDelete auf
        /// </summary>
        private async void Delete()
        {
            try
            {
                await _room.Delete();
                _onDelete?.Invoke(_room);
            }
            catch (DeleteNotAllowedException ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Löschen des Raumes", $"{ex.Message}");
            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Löschen des Raumes", $"{ex.Message}\n{ex.StackTrace}");
            }
        }
    
        /// <summary>
        /// Setzt das EditTableViewModel auf ein neues leeres EditTableViewModel
        /// </summary>
        private void AddTable()
        {
            EditTableViewModel = new EditTableViewModel(SaveTable, _room, BookingSystemPersistence);
        }

        /// <summary>
        /// Setzt das EditTableViewModel auf ein neues EditTableViewModel mit dem übergebenen Tisch
        /// </summary>
        /// <param name="table">Tisch, welcher ausgwählt wurde</param>
        private void SelectTable(Domain.Model.Table table)
        {
            EditTableViewModel = new EditTableViewModel(SaveTable, _room, DeleteTable, table, BookingSystemPersistence);
        }

        /// <summary>
        /// Speichert den bearbeiteten Tisch in der Datenbank
        /// Fügt der Liste TableViewModels ein neues TableViewModel hinzu wenn ein neuer Tisch erstellt wurde, oder Updated ein vorhandenes mit dem jeweiligen Tisch
        /// </summary>
        /// <param name="table">Tisch, der gespeichert werden soll</param>
        private void SaveTable(Domain.Model.Table table)
        {
            if (_tables.Any(t => t.Id == table.Id))
            {
                _tables.Remove(_tables.FirstOrDefault(t => t.Id == table.Id));
            }

            _tables.Add(table);
            _tables = _tables.OrderBy(t => t.Name).ToList();

            TableViewModels = new ObservableCollection<TableViewModel>(Query.Trim().Equals(String.Empty)
                ? _tables.Select(t => new TableViewModel(t, SelectTable))
                : _tables.Where(t => t.Name.ToLower().Contains(Query.ToLower())).Select(t => new TableViewModel(t, SelectTable)));

            EditTableViewModel = new EditTableViewModel(SaveTable, _room, DeleteTable, table, BookingSystemPersistence); 
        }

        /// <summary>
        /// Aktion, die nach dem Löschen eines Tisches aufgerufen wird
        /// Löscht aus der Liste TableViewModels das TableViewModel mit dem entsprechenden Tisch
        /// </summary>
        /// <param name="table"></param>
        private void DeleteTable(Domain.Model.Table table)
        {
            var tableViewModel = TableViewModels.FirstOrDefault(t => t.Table.Id == table.Id);
            if (tableViewModel != null)
                TableViewModels.Remove(tableViewModel);

            _tables.Remove(_tables.FirstOrDefault(t => t.Id == table.Id));

            EditTableViewModel = _tables.Any()
                ? new EditTableViewModel(SaveTable, _room, DeleteTable, _tables.FirstOrDefault(),
                    BookingSystemPersistence)
                : new EditTableViewModel(SaveTable, _room, BookingSystemPersistence);
        }

        private readonly Action<Domain.Model.Room> _onSave;

        private readonly Action<Domain.Model.Room> _onDelete;

        #endregion
    }
}