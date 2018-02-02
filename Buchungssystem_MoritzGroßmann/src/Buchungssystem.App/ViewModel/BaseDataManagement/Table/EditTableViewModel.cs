using System;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using MahApps.Metro.Controls.Dialogs;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Table
{
    /// <summary>
    /// ViewModel zum Erstellen oder Editieren von Tischen
    /// </summary>
    internal class EditTableViewModel : EditViewModel
    {
        private readonly Domain.Model.Room _room;

        #region Constructors

        /// <summary>
        /// Kontruktor zum Erstellen eines neuen Tisches
        /// </summary>
        /// <param name="onSave">Methode, die beim Speichern des Tisches aufgerufen werden soll</param>
        /// <param name="room">Raum, in welchem der Tisch steht</param>
        /// <param name="bookingSystemDataPersistence">Datenbankkontext</param>
        public EditTableViewModel(Action<Domain.Model.Table> onSave, Domain.Model.Room room, IPersistBookingSystemData bookingSystemDataPersistence)
        {
            BookingSystemPersistence = bookingSystemDataPersistence;

            HeaderText = "Neuen Tisch anlegen";
            Edit = true;
            Name = String.Empty;
            Places = 1;

            _onSave = onSave;
            _room = room;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
        }

        /// <summary>
        /// Kontruktor zum Bearbeiten eines Vorhandenen Tisches
        /// </summary>
        /// <param name="onSave">Methode, die beim Speichern des Tisches aufgerufen werden soll</param>
        /// <param name="room">Raum, in welchen der Tisch steht</param>
        /// <param name="onDelete">Methode, die beim Löschen des Tisches aufgerufen werden soll</param>
        /// <param name="table">Tisch, welcher bearbeitet werden soll</param>
        /// <param name="bookingSystemDataPersistence">Datenbankkontext</param>
        public EditTableViewModel(Action<Domain.Model.Table> onSave, Domain.Model.Room room, Action<Domain.Model.Table> onDelete, Domain.Model.Table table, IPersistBookingSystemData bookingSystemDataPersistence)
        {
            BookingSystemPersistence = bookingSystemDataPersistence;

            HeaderText = $"{table.Name} bearbeiten";
            Id = table.Id;

            _table = table;

            Name = table.Name;
            Places = table.Places;

            _onSave = onSave;
            _onDelete = onDelete;
            _room = room;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Repräsentiert die Id des Tisches
        /// </summary>
        public int Id { get; }

        private readonly Domain.Model.Table _table;

        private int _places;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt

        /// <summary>
        /// Anzahl Sitzplätze an dem Tisch
        /// </summary>
        public int Places
        {
            get => _places;
            set
            {

                if (value < 1)
                {
                    AddError(nameof(Places), "Der Tisch muss mindestens einen Platz haben");
                    RaisePropertyChanged(nameof(HasErrors));
                }
                else
                {
                    RemoveError(nameof(Places));
                    RaisePropertyChanged(nameof(HasErrors));

                }
                SetProperty(ref _places, value, nameof(Places));
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Erstellt einen Neuen Tisch aus den Eingegebenen Paramtern und übergibt diesen an die im kontrutor übergebene Methode onSave
        /// </summary>
        private async void Save()
        {
            ShowProgressbar = true;

            try
            {
                var table = new Domain.Model.Table
                {
                    Id = Id,
                    Name = Name,
                    Places = Places,
                    Occupied = false,
                    Room = _room,
                    Persistence = BookingSystemPersistence
                };

                var t = await table.Persist();

                _onSave?.Invoke(t);
            }
            catch (ModelExistException ex)
            {
                AddError(nameof(Name), ex.Message);
            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Error", ex.Message);
            }
            finally
            {
                ShowProgressbar = false;
            }
        }

        /// <summary>
        /// Löscht einen Tisch
        /// Ruft die im Kontruktor übergebene Funktion onDelete auf und übergibt den gelöschten Tisch
        /// </summary>
        private async void Delete()
        {
            try
            {

                await _table.Delete();
                _onDelete?.Invoke(_table);

            }
            catch (DeleteNotAllowedException ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Speichern des Tisches", $"{ex.Message}");
            }
            catch (Exception ex)
            {
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Fehler beim Löschen des Tisches", $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private readonly Action<Domain.Model.Table> _onDelete;

        private readonly Action<Domain.Model.Table> _onSave;

        #endregion
    }
}
