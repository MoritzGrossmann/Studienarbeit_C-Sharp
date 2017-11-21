using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Repository;
using Unity;
using Unity.Attributes;

namespace Buchungssystem.App.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        [InjectionConstructor]
        public MainViewModel(IUnityContainer container)
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
