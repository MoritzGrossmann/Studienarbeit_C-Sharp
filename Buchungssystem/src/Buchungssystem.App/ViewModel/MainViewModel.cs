using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.BaseDataManagement;
using Buchungssystem.App.ViewModel.RoomView;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.TestRepository;
using MahApps.Metro.Controls.Dialogs;
using Unity.Injection;

namespace Buchungssystem.App.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly IPersistBookingSystemData _bookingSystemDataPersistence;

        private readonly IDialogCoordinator _dialogCoordinator;

        #region Constructor


        public MainViewModel()
        {
            _bookingSystemDataPersistence = new TestPersitence();

            _currentViewModel = new RoomListViewModel(_bookingSystemDataPersistence.Rooms());
        }


        public MainViewModel(IPersistBookingSystemData bookingSystemDataPersistence, IDialogCoordinator instance)
        {
            _dialogCoordinator = instance;

            _bookingSystemDataPersistence = bookingSystemDataPersistence;

            _currentViewModel = new RoomListViewModel(_bookingSystemDataPersistence.Rooms());

            ToBaseDataCommand = new RelayCommand(ToBaseData);
        }

        #endregion

        #region Properties

        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel.Equals(value)) return;
                _currentViewModel = value;
                RaisePropertyChanged(nameof(CurrentViewModel));
            }
        }

        #endregion

        #region Commands
   
        public ICommand ToBaseDataCommand {get;}

        #endregion

        #region Actions

        private void ToBaseData()
        {
            //_dialogCoordinator.ShowMessageAsync(this, "fjrebhf", "gehe zu basedata");
            CurrentViewModel = new BaseDataManagementViewModel(_bookingSystemDataPersistence);
        }

        #endregion

    }
}
