using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.Annotations;
using Buchungssystem.App.ViewModel;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;

namespace Buchungssystem.App
{

    /// <summary>
    /// View Model for each Raum
    /// </summary>
    internal class RaumViewModel : BaseViewModel
    {

        #region Properties
        public Raum Raum
        {
            get => Raum;
            set
            {
                if (Raum.Equals(value)) return;
                this.Raum = value;
                RaisePropertyChanged(nameof(Raum));
            }
        }

        /// <summary>
        /// Repräsentiert die Tische, die in einem Raum stehen
        /// </summary>

        public ObservableCollection<TischViewModel> Tische
        {
            get => Tische;
            set
            {
                if (Tische.Equals(value)) return;
                this.Tische = value;
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

        #endregion

        #region Contructor

        public RaumViewModel(Raum raum)
        {
            this.ChooseTableCommand = new RelayCommand(ChooseTable);
            Raum = raum;
            Tische = new ObservableCollection<TischViewModel>(
                new StammdatenPersistenz().Tische(raum).Select(tisch => new TischViewModel(tisch))
            );
        }

        #endregion

        #region Commands

        public ICommand ChooseTableCommand;

        public void ChooseTable()
        {
            
        }

        #endregion
    }
}
