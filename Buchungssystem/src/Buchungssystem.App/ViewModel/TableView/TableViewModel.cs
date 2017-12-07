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

        public decimal Price
        {
            get
            {
                decimal sum = 0;
                Table.Bookings.Where(b => b.Status == BookingStatus.Open).ForEach(b => sum += b.Price);
                return sum;
            }
        }

        public Brush Color
        {
            get => (Brush) new BrushConverter().ConvertFrom(Table.Occupied ? "#f44242" : "#000000");
        }

        public string LastBookingTime => Table.Bookings.Any() ? ((int) (DateTime.Now.Subtract(Table.Bookings.LastOrDefault().Created).TotalMinutes)).ToString() : "";

        #endregion

        #region Contructor

        public TableViewModel(Table table, EventHandler<Table> onTableSelected, EventHandler<Table> onStatusChanged)
        {
            _onTableSelected = onTableSelected;
            _onStatusChanged = onStatusChanged;
            _table = table;


            SelectCommand = new RelayCommand(Select);
            ChangeStatusCommand = new RelayCommand(ChangeStatus);
            RaisePropertyChanged(nameof(Price));
        }

        private readonly EventHandler<Table> _onTableSelected;

        private readonly EventHandler<Table> _onStatusChanged;

        #endregion

        public ICommand SelectCommand { get; }

        public void Select()
        {
            _onTableSelected?.Invoke(this,_table);
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
            RaisePropertyChanged(nameof(Color));
            RaisePropertyChanged(nameof(Table.Occupied));
            _onStatusChanged.Invoke(this,Table);
        }
    }
}
