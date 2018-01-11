using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    internal abstract class EditViewModel : BaseViewModel
    {
        protected IPersistBookingSystemData BookingSystemPersistence;

        #region Properties

        private string _headerText;

        /// <summary>
        /// Repräsentiert den Text, der als Überschrift in einer Bearbeitungsansicht erscheint
        /// </summary>
        public string HeaderText
        {
            get => _headerText;
            set => SetProperty(ref _headerText, value, nameof(HeaderText));
        }

        private string _name;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt

        /// <summary>
        /// Repräsemtiert den Namen eines Elements
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
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

                    SetProperty(ref _name, value, nameof(Name));
                }
            }
        }

        private bool _edit;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt

        /// <summary>
        /// Zeigt an, ob Editieransicht oder Leseansicht angezeigt werden soll
        /// </summary>
        public bool Edit
        {
            get => _edit;
            set => SetProperty(ref _edit, value, nameof(Edit));
        }

        private bool _showProgressbar;

        /// <summary>
        /// Zeigt an, ob die Progressbar sichtbar ist
        /// </summary>
        public bool ShowProgressbar
        {
            get => _showProgressbar;
            set => SetProperty(ref _showProgressbar, value, nameof(ShowProgressbar));
        }

        #endregion

        #region Commands

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommando zum Speichern eines neuen oder eines Bearbeiteten Elements
        /// </summary>
        public ICommand SaveCommand { get; set; }

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommando zum Umschalten zwischen Editier- und Leseansicht
        /// </summary>
        public ICommand EditCommand { get; set; }

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommand zum Löschen eines Elements
        /// </summary>
        public ICommand DeleteCommand { get; set; }

        #endregion

        #region Actions

        protected void ToggleEdit()
        {
            Edit = !Edit;
        }

        #endregion
    }
}
