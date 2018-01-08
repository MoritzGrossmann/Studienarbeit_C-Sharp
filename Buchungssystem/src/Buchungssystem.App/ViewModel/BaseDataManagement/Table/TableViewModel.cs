using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.BaseDataManagement.Table
{
    internal class TableViewModel : BaseViewModel
    {
        private Domain.Model.Table _table;

        public Domain.Model.Table Table
        {
            get => _table;
            set => SetProperty(ref _table, value, nameof(Table));
        }

        public TableViewModel(Domain.Model.Table table, Action<Domain.Model.Table> onSelect)
        {
            Table = table;
            _onSelect = onSelect;
            SelectCommand = new RelayCommand(Select);
        }

        private readonly Action<Domain.Model.Table> _onSelect;

        private void Select()
        {
            _onSelect?.Invoke(_table);
        }

        public ICommand SelectCommand { get; }
    }
}
