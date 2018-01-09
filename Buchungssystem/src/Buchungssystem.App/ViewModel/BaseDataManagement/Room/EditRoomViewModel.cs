using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement.Table;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Room
{
    internal class EditRoomViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemPerstence;

        public int Id { get; }

        private readonly Domain.Model.Room _room;

        #region Properties

        private string _name;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt

        /// <summary>
        /// Name des Raumes, welcher in der Textbox erscheint
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    if (value.Trim().Equals(String.Empty))
                    {
                        AddError(nameof(Name), "Der Name darf nicht leer sein");
                        RaisePropertyChanged(nameof(HasErrors));
                    }
                    else
                    {
                        RemoveError(nameof(Name));
                        RaisePropertyChanged(nameof(HasErrors));

                    }
                }
                SetProperty(ref _name, value, nameof(Name));
            }
        }

        private bool _edit;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt

        /// <summary>
        /// Zeigt an, ob Editieransicht oder Leseansicht angezeigt werden soll
        /// </summary>
        public bool Edit
        {
            get => _edit;
            set => SetProperty(ref _edit, value, nameof(Edit));
        }

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

        #endregion

        #region Contructor

        /// <summary>
        /// Konstruktor zum Erstellen eines neuen Raumes
        /// </summary>
        /// <param name="onSave">Eventhandler, welcher beim Speichern ausgelößt wird</param>
        /// <param name="bookingSystemPerstence">Kontext zurt Datenbank</param>
        public EditRoomViewModel(Action<Domain.Model.Room> onSave, IPersistBookingSystemData bookingSystemPerstence)
        {
            _name = String.Empty;
            _edit = true;
            _bookingSystemPerstence = bookingSystemPerstence;
            _onSave = onSave;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);
            AddTableCommand = new RelayCommand(AddTable);

            TableViewModels = new ObservableCollection<TableViewModel>();
            EditTableViewModel = null;
        }

        /// <summary>
        /// Kontruktor zum Bearbeiten eines neuen Raumes
        /// </summary>
        /// <param name="onSave"></param>
        /// <param name="onDelete"></param>
        /// <param name="bookingSystemPerstence"></param>
        /// <param name="room"></param>
        public EditRoomViewModel(Action<Domain.Model.Room> onSave, Action<Domain.Model.Room> onDelete,
            IPersistBookingSystemData bookingSystemPerstence, Domain.Model.Room room)
        {
            Id = room.Id;
            _room = room;
            _name = room.Name;

            _bookingSystemPerstence = bookingSystemPerstence;

            _onSave = onSave;
            _onDelete = onDelete;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);
            AddTableCommand = new RelayCommand(AddTable);

            if (room.Tables != null)
                TableViewModels = new ObservableCollection<TableViewModel>(room.Tables.AsEnumerable().Select(t => new TableViewModel(t, SelectTable)));
            else
                TableViewModels = new ObservableCollection<TableViewModel>();    

            if (TableViewModels.Any())
                EditTableViewModel = new EditTableViewModel(SaveTable, DeleteTable, TableViewModels.FirstOrDefault()?.Table);
        }

        #endregion

        #region Commands

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommando zum Speichern eines neuen oder eines Bearbeiteten Raumes
        /// </summary>
        public ICommand SaveCommand { get; }

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommando zum Umschalten zwischen Editier- und Leseansicht
        /// </summary>
        public ICommand EditCommand { get; }

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommand zum Löschen eines Raumes
        /// </summary>
        public ICommand DeleteCommand { get; }

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommando zum Hinzufügen eines neuen Tisches
        /// </summary>

        public ICommand AddTableCommand { get; }

        #endregion

        #region Actions

        private void ToggleEdit()
        {
            Edit = !Edit;
        }

        private void Save()
        {
            try
            {
                var room = new Domain.Model.Room() {Id = Id, Name = Name, Persistence = _bookingSystemPerstence}.Persist();

                _onSave?.Invoke(room);
            }
            catch (ModelExistException)
            {
                AddError(nameof(Name), $"Der Name {Name} wurde schon vergeben");
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        private void Delete()
        {
            _room?.Delete();
            _onDelete?.Invoke(_room);
        }

        private void AddTable()
        {
            EditTableViewModel = new EditTableViewModel(SaveTable);
        }

        private void SelectTable(Domain.Model.Table table)
        {
            EditTableViewModel = new EditTableViewModel(SaveTable, DeleteTable, table);
        }

        private void SaveTable(Domain.Model.Table table)
        {
            table.Room = _room;
            table.Persistence = _bookingSystemPerstence;

            table = table.Persist();
            

            var tableViewModel = TableViewModels.FirstOrDefault(t => t.Table.Id == table.Id);
            if (tableViewModel != null)
                tableViewModel.Table = table;
            else
                TableViewModels.Add(new TableViewModel(table, SelectTable));

            EditTableViewModel = new EditTableViewModel(SaveTable, DeleteTable, table);
        }

        private void DeleteTable(Domain.Model.Table table)
        {
            var tableViewModel = TableViewModels.FirstOrDefault(t => t.Table.Id == table.Id);
            if (tableViewModel != null)
                TableViewModels.Remove(tableViewModel);
        }

        private readonly Action<Domain.Model.Room> _onSave;

        private readonly Action<Domain.Model.Room> _onDelete;

        #endregion
    }
}
