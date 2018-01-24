using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Room
{
    /// <summary>
    /// ViewModel für die Übersicht der Raum-Verwaltung
    /// </summary>
    internal class ChangeRoomsViewModel : BaseViewModel
    {
        #region Properties

        private readonly IPersistBookingSystemData _bookingSystemPersistence;

        /// <summary>
        /// Liste aller Existierenden Räume
        /// </summary>
        public ObservableCollection<RoomViewModel> RoomViewModels { get; set; }

        private BaseViewModel _actualRoomViewModel;

        /// <summary>
        /// Repträsentiert den aktuell bearbeiteten Raum
        /// </summary>
        public BaseViewModel ActualRoomViewModel
        {
            get => _actualRoomViewModel;
            set => SetProperty(ref _actualRoomViewModel, value, nameof(ActualRoomViewModel));
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        /// <param name="bookingSystemPersistence">Datenbankkontext</param>
        public ChangeRoomsViewModel(IPersistBookingSystemData bookingSystemPersistence)
        {
            _bookingSystemPersistence = bookingSystemPersistence;

            RoomViewModels = new ObservableCollection<RoomViewModel>(_bookingSystemPersistence.Rooms().Select(r => new RoomViewModel(r, SelectRoom)));

            AddRoomCommand = new RelayCommand(AddRoom);

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit RoomViewModels.Any() ausgeschlossen
            ActualRoomViewModel = RoomViewModels.Any() ? new EditRoomViewModel(Save, Delete, _bookingSystemPersistence, RoomViewModels.FirstOrDefault().Room) : null;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Aktion, die beim Auswählen eines neuen Raumes ausgeführt wird
        /// Setzt das ActualRoomViewModel auf ein neues EditRoomViewModel mit dem ausgewählten Raum
        /// </summary>
        /// <param name="room">Raum, welcher ausgewählt wurde</param>
        private void SelectRoom(Domain.Model.Room room)
        {
            ActualRoomViewModel = new EditRoomViewModel(Save, Delete, _bookingSystemPersistence, room);
        }

        /// <summary>
        /// Aktion, die beim Speichern eines neuen Raumes ausgeführt wird
        /// Fügt der Liste RoomViewModels ein neues RoomViewModel hinzu wenn ein neuer Raum erstellt wurde oder Updated das RoomViewModel mit dem entsprechenden Raum
        /// </summary>
        /// <param name="room">Raum, welcher gespeichert wurde</param>
        private void Save(Domain.Model.Room room)
        {
            var roomViewModel = RoomViewModels.FirstOrDefault(r => r.Room.Id == room.Id);

            if (roomViewModel != null)
                roomViewModel.Room = room;
            else
                RoomViewModels.Add(new RoomViewModel(room, SelectRoom));

            ActualRoomViewModel = new EditRoomViewModel(Save, Delete, _bookingSystemPersistence, room);
        }

        /// <summary>
        /// Aktion, die beim Löschen eines Raumes aufgerufen wird
        /// Löscht aus der Liste RoomViewModels das RoomViewModel mit dem entsprechenden Raum
        /// </summary>
        /// <param name="room">Raum, welcher gelöscht wurde</param>
        private void Delete(Domain.Model.Room room)
        {
            var roomViewModel = RoomViewModels.FirstOrDefault(r => r.Room.Id == room.Id);
            RoomViewModels.Remove(roomViewModel);

            // ReSharper disable once PossibleNullReferenceException : NullReferenceException wird mit RoomViewModels.Any() ausgeschlossen
            ActualRoomViewModel = RoomViewModels.Any() ? new EditRoomViewModel(Save, Delete, _bookingSystemPersistence, RoomViewModels.FirstOrDefault().Room) : new EditRoomViewModel(Save, _bookingSystemPersistence);
        }

        /// <summary>
        /// Anlegen eines neuen Raumes
        /// ActualRoomViewModel wird auf ein neues EditRoomViewModel gesetzt
        /// </summary>
        private void AddRoom()
        {
            ActualRoomViewModel = new EditRoomViewModel(Save, _bookingSystemPersistence);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Kommando zum Hinzufügen eines neuen Raumes
        /// </summary>
        public ICommand AddRoomCommand { get; }

        #endregion

    }
}
