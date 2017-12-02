using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel.TableView
{
    internal class TableViewModel : BaseViewModel
    {
        #region Properties

        private Table _table;
        public Table Table
        {
            get => _table;
            set
            {
                if (_table.Equals(value)) return;
                _table = value;
                RaisePropertyChanged(nameof(Table));
            }
        }

        public string Name
        {
            get => _table.Name;
            set
            {
                if (_table.Name.Equals(value)) return;
                _table.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        #endregion

        #region Contructor

        public TableViewModel(Table table, Action<Table> onTableSelected)
        {
            _onTableSelected = onTableSelected;
            _table = table;

            SelectCommand = new RelayCommand(Select);
            ChangeStatusCommand = new RelayCommand(ChangeStatus);

        }

        private readonly Action<Table> _onTableSelected;

        #endregion

        public ICommand SelectCommand { get; }

        public void Select()
        {
            _onTableSelected?.Invoke(_table);
        }

        public ICommand ChangeStatusCommand { get; }

        private void ChangeStatus()
        {
            if (Table.Occupied)
            {
                Table.Clear();
            }
            else
            {
                Table.Occupy();
            }
        }
    }
}
