﻿using System;
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
            StammdatenPersistenz = new StammdatenPersistenz();
            this.ShowRoomCommand = new RelayCommand(ShowRoom);
            _raeume = new ObservableCollection<RaumViewModel>(StammdatenPersistenz.Raeume().Select(raum => new RaumViewModel(raum)));
            //Raeume[0].ChooseTableCommand.Execute(null);
        }

        private IchPersistiereStammdaten StammdatenPersistenz;

        #region Propertys

        private ObservableCollection<RaumViewModel> _raeume;
        public ObservableCollection<RaumViewModel> Raeume
        {
            get => _raeume;
            set
            {
                if (_raeume == value) return;
                _raeume = value;
                RaisePropertyChanged(nameof(Raeume));
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
