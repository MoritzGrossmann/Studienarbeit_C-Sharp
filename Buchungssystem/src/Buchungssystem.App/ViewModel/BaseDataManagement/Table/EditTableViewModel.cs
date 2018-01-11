using System;
using Buchungssystem.App.ViewModel.Base;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Table
{
    internal class EditTableViewModel : EditViewModel
    {
        #region Constructors

        /// <summary>
        /// Kontruktor zum Erstellen eines neuen Tisches
        /// </summary>
        /// <param name="onSave"></param>
        public EditTableViewModel(Action<Domain.Model.Table> onSave)
        {
            HeaderText = "Neuen Tisch anlegen";
            Edit = true;
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
            HeaderText = $"{table.Name} bearbeiten";
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

        #endregion

        #region Actions

        private void Save()
        {
            ShowProgressbar = true;
            var table = new Domain.Model.Table() {Id = Id, Name = Name, Places = Places, Occupied = false};
            _onSave?.Invoke(table);
        }

        private void Delete()
        {
            _table.Delete();
            _onDelete?.Invoke(_table);
        }

        private readonly Action<Domain.Model.Table> _onDelete;

        private readonly Action<Domain.Model.Table> _onSave;

        #endregion
    }
}
