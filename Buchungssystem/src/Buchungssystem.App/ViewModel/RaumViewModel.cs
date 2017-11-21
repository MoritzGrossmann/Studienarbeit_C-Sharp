using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;

namespace Buchungssystem.App.ViewModel
{

    /// <summary>
    /// View Model for each Raum
    /// </summary>
    internal class RaumViewModel : BaseViewModel
    {

        #region Properties

        private Raum _raum;
        public Raum Raum
        {
            get => _raum;

            set
            {
                if (_raum.Equals(value)) return;
                _raum = value;
                RaisePropertyChanged(nameof(Raum));
            }
        }

        /// <summary>
        /// Repräsentiert die Tische, die in einem Raum stehen
        /// </summary>

        private ObservableCollection<TischViewModel> _tische;
        public ObservableCollection<TischViewModel> Tische
        {
            get => _tische;
            set
            {
                if (_tische.Equals(value)) return;
                _tische = value;
                RaisePropertyChanged(nameof(Tische));
            }
        }


    /// <summary>
    /// Name des Raumes
    /// </summary>
        public string Name
        {
            get => _raum.Name;
            set
            {
                if (Equals(_raum.Name, value)) return;
                _raum.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public bool CanSelect
        {
            get => Tische.Any();
        }

        private bool _selected;
        public bool Selected
        {
            get => Selected;
            set
            {
                if (_selected.Equals(value)) return;
                _selected = value;
                RaisePropertyChanged(nameof(Selected));
                if (value) Select();
            }
        }

        #endregion

        #region Contructor

        public RaumViewModel(Raum raum)
        {
            ChooseTableCommand = new RelayCommand(ChooseTable);
            SelectCommand = new RelayCommand(Select);
            _raum = raum;
            _tische = new ObservableCollection<TischViewModel>(
                new StammdatenPersistenz().Tische(raum).Select(tisch => new TischViewModel(tisch))
            );
        }

        #endregion

        #region Commands

        public ICommand SelectCommand;

        public void Select()
        {
            
        }

        public ICommand ChooseTableCommand;

        public void ChooseTable()
        {
            
        }

        #endregion
    }
}