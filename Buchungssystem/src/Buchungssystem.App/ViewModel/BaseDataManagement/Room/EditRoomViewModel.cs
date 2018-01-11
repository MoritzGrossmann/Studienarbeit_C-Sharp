using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement.Table;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Room
{
    internal class EditRoomViewModel : EditViewModel
    {
        public int Id { get; }

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

        #endregion

        #region Contructor

        /// <summary>
        /// Konstruktor zum Erstellen eines neuen Raumes
        /// </summary>
        /// <param name="onSave">Action welche beim Speichern ausgelößt wird</param>
        /// <param name="bookingSystemPersistence"></param>
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

            TableViewModels = new ObservableCollection<TableViewModel>();
            EditTableViewModel = null;
        }

        /// <summary>
        /// Kontruktor zum Bearbeiten eines neuen Raumes
        /// </summary>
        /// <param name="onSave"></param>
        /// <param name="onDelete"></param>
        /// <param name="bookingSystemPersistence"></param>
        /// <param name="room"></param>
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
        /// Kommando zum Hinzufügen eines neuen Tisches
        /// </summary>

        public ICommand AddTableCommand { get; }

        #endregion

        #region Actions

        private void Save()
        {
            try
            {

                var room = new Domain.Model.Room() {Id = Id, Name = Name, Persistence = BookingSystemPersistence};

                ShowProgressbar = true;

                TaskAwaiter<Domain.Model.Room> awaiter = SaveTask(room).GetAwaiter();

                awaiter.OnCompleted(() =>
                {
                    var r = awaiter.GetResult();
                    ShowProgressbar = false;
                    _onSave?.Invoke(r);
                });
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
            table.Persistence = BookingSystemPersistence;

            var table1 = table;

            TaskAwaiter<Domain.Model.Table> awaiter = Task.Run(() => table1.Persist()).GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                var tab = awaiter.GetResult();
                    var tableViewModel = TableViewModels.FirstOrDefault(t => t.Table.Id == tab.Id);
                    if (tableViewModel != null)
                        tableViewModel.Table = tab;
                    else
                        TableViewModels.Add(new TableViewModel(tab, SelectTable));

                    EditTableViewModel = new EditTableViewModel(SaveTable, DeleteTable, tab);
                });    
        }

        private Task<Domain.Model.Room> SaveTask(Domain.Model.Room room)
        {
            return Task.Run(() => room.Persist());
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
