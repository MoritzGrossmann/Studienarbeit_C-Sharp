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
                SetProperty(ref _table, value, nameof(Table));
                RaisePropertyChanged(nameof(Name));
                RaisePropertyChanged(nameof(Price));
            }
        }

        public string Name => _table.Name;

        public decimal Price
        {
            get
            {
                decimal sum = 0;
                Table.Bookings.Where(b => b.Status == BookingStatus.Open).ForEach(b => sum += b.Price);
                return sum;
            }
        }

        public Brush Color => (Brush) new BrushConverter().ConvertFrom(Table.Occupied ? "#f44242" : "#000000");

        public string LastBookingTime => Table.Bookings.Any() ? ((int) (DateTime.Now.Subtract(Table.Bookings.LastOrDefault().Created).TotalMinutes)).ToString() : "";

        #endregion

        #region Constructor

        public TableViewModel(Table table, EventHandler<Table> onTableSelected, EventHandler<Table> onStatusChanged)
        {
            _onTableSelected = onTableSelected;
            _onStatusChanged = onStatusChanged;
            _table = table;


            SelectCommand = new RelayCommand(Select);
            ChangeStatusCommand = new RelayCommand(ChangeStatus);
        }

        private readonly EventHandler<Table> _onTableSelected;

        private readonly EventHandler<Table> _onStatusChanged;

        #endregion

        public ICommand SelectCommand { get; }

        private void Select()
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
