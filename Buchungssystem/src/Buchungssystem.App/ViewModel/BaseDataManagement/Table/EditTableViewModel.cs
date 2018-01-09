using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Table
{
    internal class EditTableViewModel : BaseViewModel
    {
        #region Constructors

        /// <summary>
        /// Kontruktor zum Erstellen eines neuen Tisches
        /// </summary>
        /// <param name="onSave"></param>
        public EditTableViewModel(Action<Domain.Model.Table> onSave)
        {
            _edit = true;

            Name = String.Empty;
            Places = 1;

            _onSave = onSave;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
        }

        /// <summary>
        /// Kontruktor zum Bearbeiten eines Vorhandenen Tisches
        /// </summary>
        /// <param name="onSave"></param>
        /// <param name="onDelete"></param>
        /// <param name="table"></param>
        public EditTableViewModel(Action<Domain.Model.Table> onSave, Action<Domain.Model.Table> onDelete, Domain.Model.Table table)
        {
            Id = table.Id;

            _table = table;

            Name = table.Name;
            Places = table.Places;

            _onSave = onSave;
            _onDelete = onDelete;

            SaveCommand = new RelayCommand(Save);
            EditCommand = new RelayCommand(ToggleEdit);
            DeleteCommand = new RelayCommand(Delete);
        }

        #endregion

        #region Properties

        public int Id { get; }

        private readonly Domain.Model.Table _table;

        private string _name;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt

        /// <summary>
        /// Name des Tisches
        /// </summary>
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

        private int _places;

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt

        /// <summary>
        /// Anzahl Sitzplätze an dem Tisch
        /// </summary>
        public int Places
        {
            get => _places;
            set
            {

                if (value < 1)
                {
                    AddError(nameof(Places), "Der Tisch muss mindestens einen Platz haben");
                    RaisePropertyChanged(nameof(HasErrors));
                }
                else
                {
                    RemoveError(nameof(Places));
                    RaisePropertyChanged(nameof(HasErrors));

                }
                SetProperty(ref _places, value, nameof(Places));
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

        #endregion

        #region Actions

        private void Save()
        {
            var table = new Domain.Model.Table() {Id = Id, Name = Name, Places = Places, Occupied = false};
            _onSave?.Invoke(table);
        }

        private void Delete()
        {
            _table.Delete();
            _onDelete?.Invoke(_table);
        }

        private void ToggleEdit()
        {
            Edit = !Edit;
        }

        private readonly Action<Domain.Model.Table> _onDelete;

        private readonly Action<Domain.Model.Table> _onSave;

        #endregion

        #region Commands

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommando zum Speichern eines neuen oder eines Bearbeiteten Tisches
        /// </summary>
        public ICommand SaveCommand { get; }

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommando zum Umschalten zwischen Editier- und Leseansicht
        /// </summary>
        public ICommand EditCommand { get; }

        // ReSharper disable once MemberCanBePrivate.Global : Datenkontext wird zur Laufzeit gesetzt
        // ReSharper disable once UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Kommand zum Löschen eines Tisches
        /// </summary>
        public ICommand DeleteCommand { get; }

        #endregion
    }
}
