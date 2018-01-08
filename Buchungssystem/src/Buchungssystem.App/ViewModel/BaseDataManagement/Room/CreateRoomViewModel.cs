using System;
using System.Collections.Generic;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Room
{
    internal class CreateRoomViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemPerstence;

        private int _id;

        private Domain.Model.Room _room;

        private string _name;

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

        public bool Edit
        {
            get => _edit;
            set => SetProperty(ref _edit, value, nameof(Edit));
        }

        public CreateRoomViewModel(EventHandler<Domain.Model.Room> onSave, IPersistBookingSystemData bookingSystemPerstence)
        {
            Name = "";
            _bookingSystemPerstence = bookingSystemPerstence;
            _onSave = onSave;
            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);
        }

        public CreateRoomViewModel(EventHandler<Domain.Model.Room> onSave, EventHandler<Domain.Model.Room> onDelete,
            IPersistBookingSystemData bookingSystemPerstence, Domain.Model.Room room)
        {
            _id = room.Id;
            _room = room;
            _name = room.Name;

            _bookingSystemPerstence = bookingSystemPerstence;

            _onSave = onSave;
            _onDelete = onDelete;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);
        }

        public ICommand SaveCommand { get; }

        public ICommand EditCommand { get; }

        public ICommand DeleteCommand { get; }

        private void Save()
        {
            try
            {
                _onSave?.Invoke(this,
                    new Domain.Model.Room() {Id = _id, Name = _name, Persistence = _bookingSystemPerstence, Tables = new List<Table>()});
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
            _onDelete?.Invoke(this, _room);
        }

        private readonly EventHandler<Domain.Model.Room> _onSave;

        private readonly EventHandler<Domain.Model.Room> _onDelete;

        #region Actions

        private void ToggleEdit()
        {
            Edit = !Edit;
        }

        #endregion
    }
}
