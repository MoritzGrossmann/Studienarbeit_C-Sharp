using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement
{
    internal class TableViewModel : BaseViewModel
    {
        private Table _table;

        public Table Table
        {
            get => _table;
            set => SetProperty(ref _table, value, nameof(Table));
        }

        public TableViewModel(Table table, EventHandler<Table> onDelete)
        {
            Table = table;
            DeleteCommand = new RelayCommand(() => onDelete.Invoke(this, _table));
        }

        private EventHandler<Table> _onDelete;

        public ICommand DeleteCommand { get; }

        public ICommand SaveCommand { get; }
    }
}
