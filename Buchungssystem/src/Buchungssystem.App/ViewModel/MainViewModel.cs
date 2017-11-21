using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.Annotations;
using Buchungssystem.App.ViewModel;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;

namespace Buchungssystem.App.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            BaseDataPersistence = new BaseDataPersitence();
            this.ShowRoomCommand = new RelayCommand(ShowRoom);
            _rooms = new ObservableCollection<RoomViewModel>(BaseDataPersistence.Rooms().Select(raum => new RoomViewModel(raum)));
            //Rooms[0].ChooseTableCommand.Execute(null);
        }

        private IPersistBaseData BaseDataPersistence;

        #region Propertys

        private ObservableCollection<RoomViewModel> _rooms;
        public ObservableCollection<RoomViewModel> Rooms
        {
            get => _rooms;
            set
            {
                if (_rooms == value) return;
                _rooms = value;
                RaisePropertyChanged(nameof(Rooms));
            }
        }

        #endregion

        #region Commands

        public ICommand ShowRoomCommand;

        public void ShowRoom()
        {
            
        }

        #endregion
    }
}
