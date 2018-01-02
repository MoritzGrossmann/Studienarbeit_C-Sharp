using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    internal class CreateRoomViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemPerstence;

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, nameof(Name));
        }

        private string _errorText;

        public string ErrorText
        {
            get => _errorText;
            set => SetProperty(ref _errorText, value, nameof(ErrorText));
        }

        private bool _hasErrors;

        public bool HasErrors
        {
            get => _hasErrors;
            set => SetProperty(ref _hasErrors, value, nameof(Name));
        }

        public CreateRoomViewModel(EventHandler<Room> onSave, IPersistBookingSystemData bookingSystemPerstence)
        {
            Name = "";
            _bookingSystemPerstence = bookingSystemPerstence;
            _onSave = onSave;
            SaveCommand = new RelayCommand(Save);
        }

        public CreateRoomViewModel()
        {
            Name = "";
        }

        public ICommand SaveCommand { get; }

        private void Save()
        {
            HasErrors = false;
            try
            {
                _onSave?.Invoke(this,
                    new Room() {Name = _name, Persistence = _bookingSystemPerstence, Tables = new List<Table>()});
            }
            catch (ModelExistException modelExistException)
            {
                ErrorText = modelExistException.Message;
                HasErrors = true;
            }
        }

        private readonly EventHandler<Room> _onSave;
    }
}
