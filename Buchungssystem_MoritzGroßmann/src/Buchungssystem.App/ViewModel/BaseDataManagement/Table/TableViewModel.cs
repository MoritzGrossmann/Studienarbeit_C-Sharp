using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Table
{
    /// <summary>
    /// ViewModel zur Anzeige eines Tisches in der Tischliste der Raumverlwatung
    /// </summary>
    internal class TableViewModel : BaseViewModel
    {
        private Domain.Model.Table _table;

        /// <summary>
        /// Repräsentiert den Tisch im TableViewModel
        /// </summary>
        public Domain.Model.Table Table
        {
            get => _table;
            set => SetProperty(ref _table, value, nameof(Table));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table">Tisch</param>
        /// <param name="onSelect">Methode, die bei der Auswahl des Tisches ausgeführt werden soll</param>
        public TableViewModel(Domain.Model.Table table, Action<Domain.Model.Table> onSelect)
        {
            Table = table;
            _onSelect = onSelect;
            SelectCommand = new RelayCommand(Select);
        }

        private readonly Action<Domain.Model.Table> _onSelect;

        /// <summary>
        /// Ruft die im Kontruktor übergebene Methode onSelect auf und übergibt den Tisch
        /// </summary>
        private void Select()
        {
            _onSelect?.Invoke(_table);
        }

        /// <summary>
        /// Kommando zur Auswahl des Tisches
        /// </summary>
        public ICommand SelectCommand { get; }
    }
}
