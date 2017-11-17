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
        //public Raum Raum
        //{
        //    get => Raum;

        //    set
        //    {
        //        if (Raum.Equals(value)) return;
        //        Raum = value;
        //        RaisePropertyChanged(nameof(Raum));
        //    }
        //}

        /// <summary>
        /// Repräsentiert die Tische, die in einem Raum stehen
        /// </summary>

        public ObservableCollection<TischViewModel> Tische
        {
            get => Tische;
            set
            {
                if (Tische.Equals(value)) return;
                Tische = value;
                RaisePropertyChanged(nameof(Tische));
            }
        }


        /// <summary>
        /// Name des Raumes
        /// </summary>
        public string Name
        {
            get => Raum.Name;
            set
            {
                if (Equals(Name, value)) return;
                Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public bool CanSelect
        {
            get => Tische.Any();
        }

        public bool IsSelected
        {
            get => IsSelected;
            set
            {
                if (IsSelected.Equals(value)) return;
                IsSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
                if (value) Select();
            }
        }

        #endregion

        #region Contructor

        public RaumViewModel(Raum raum)
        {
            ChooseTableCommand = new RelayCommand(ChooseTable);
            SelectCommand = new RelayCommand(Select);
            Raum = raum;
            Tische = new ObservableCollection<TischViewModel>(
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