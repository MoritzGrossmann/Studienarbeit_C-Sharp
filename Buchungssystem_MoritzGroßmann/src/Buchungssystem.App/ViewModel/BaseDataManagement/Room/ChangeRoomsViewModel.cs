using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Liste aller Existierenden Räume
        /// </summary>
        private ICollection<Domain.Model.Room> _rooms;

        /// <summary>
        /// Daternbankkontext
        /// </summary>
        private readonly IPersistBookingSystemData _bookingSystemPersistence;

        private ObservableCollection<RoomViewModel> _roomViewModels;

        /// <summary>
        /// Repräsentiert alle Angezeigten Räume
        /// </summary>
        public ObservableCollection<RoomViewModel> RoomViewModels
        {
            get => _roomViewModels;
            set => SetProperty(ref _roomViewModels, value, nameof(RoomViewModels));
        }

        private BaseViewModel _actualRoomViewModel;

        /// <summary>
        /// Repträsentiert den aktuell bearbeiteten Raum
        /// </summary>
        public BaseViewModel ActualRoomViewModel
        {
            get => _actualRoomViewModel;
            set => SetProperty(ref _actualRoomViewModel, value, nameof(ActualRoomViewModel));
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
                    RoomViewModels =
                        new ObservableCollection<RoomViewModel>(
                            _rooms.Select(r => new RoomViewModel(r, Select)));
                }
                else
                {
                    RoomViewModels =
                        new ObservableCollection<RoomViewModel>(
                            _rooms.Where(r => r.Name.ToLower().Contains(value.ToLower())).Select(r => new RoomViewModel(r, Select)));
                }
                SetProperty(ref _query, value, nameof(Query));

            }
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
            _rooms = bookingSystemPersistence.Rooms().OrderBy(r => r.Name).ToList();

            RoomViewModels = new ObservableCollection<RoomViewModel>(_rooms.Select(r => new RoomViewModel(r, Select)));

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
        private void Select(Domain.Model.Room room)
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
            if (_rooms.Any(r => r.Id == room.Id))
            {
                _rooms.Remove(_rooms.FirstOrDefault(r => r.Id == room.Id));
            }

            _rooms.Add(room);
            _rooms = _rooms.OrderBy(r => r.Name).ToList();

            RoomViewModels = new ObservableCollection<RoomViewModel>(
                Query.Trim().Equals(String.Empty)
                    ? _rooms.Select(r => new RoomViewModel(r, Select))
                    : _rooms.Where(r => r.Name.ToLower().Contains(Query.ToLower())).Select(r => new RoomViewModel(r, Select)));

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
