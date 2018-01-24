using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.TableView;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.RoomView
{
    /// <summary>
    /// Räpresentiert eine List von TableViewModel
    /// </summary>
    internal class TableListViewModel : BaseViewModel
    {
        #region Properties

        private ObservableCollection<TableViewModel> _tableViewModels;

        /// <summary>
        /// List mit allen TableViewModel im TableListViewModel
        /// </summary>
        public ObservableCollection<TableViewModel> TableViewModels
        {
            get => _tableViewModels;
            set => SetProperty(ref _tableViewModels, value, nameof(TableViewModels));
        }

        /// <summary>
        /// Summe der freien Tische aller TableViewModels
        /// </summary>
        public int FreeTables => TableViewModels.Count(t => !t.Table.Occupied);

        /// <summary>
        /// Summe aller freien Plätze 
        /// </summary>
        public int FreePlaces => TableViewModels.Where(t => !t.Table.Occupied).Sum(t => t.Table.Places);

        #endregion

        #region Contructor


        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        /// <param name="tables">Liste aller Tische, die im TableListViewModel erscheinen soll</param>
        /// <param name="onTableSelected">Methode, die bei der Auswahl eines Tisches aufgerufen wird</param>
        public TableListViewModel(ICollection<Table> tables, Action<Table> onTableSelected)
        {
            TableViewModels = new ObservableCollection<TableViewModel>(tables.Select(t => new TableViewModel(t, onTableSelected, TableStatusChanged)));
        }

        #endregion

        #region Actions

        /// <summary>
        /// Nimmt die änderungen an Freeplaces und FreeTables wahr, wenn sich der Status eines Tisches geändert hat
        /// </summary>
        /// <param name="table"></param>
        private void TableStatusChanged(Table table)
        {
            RaisePropertyChanged(nameof(FreeTables));
            RaisePropertyChanged(nameof(FreePlaces));
        }

        #endregion
    }
}
