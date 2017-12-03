﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.App.ViewModel.TableView;
using Buchungssystem.Domain.Model;
using Buchungssystem.TestRepository;

namespace Buchungssystem.App.ViewModel.RoomView
{
    internal class TableListViewModel : BaseViewModel
    {
        #region Properties

        private ObservableCollection<TableViewModel> _tableViewModels;

        public ObservableCollection<TableViewModel> TableViewModels
        {
            get { return _tableViewModels; }
            set { _tableViewModels = value; }
        }

        public int FreeTables => TableViewModels.Count(t => !t.Table.Occupied);

        public int FreePlaces => TableViewModels.Where(t => !t.Table.Occupied).Sum(t => t.Table.Places);

        #endregion

        #region Contructor

        public TableListViewModel(ICollection<Table> tables, Action<Table> onTableSelected)
        {
            TableViewModels = new ObservableCollection<TableViewModel>(tables.Select(t => new TableViewModel(t, onTableSelected, TableStatusChanged)));
        }

        #endregion

        #region Actions

        private void TableStatusChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(FreeTables));
            RaisePropertyChanged(nameof(FreePlaces));
        }

        #endregion
    }
}
