using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Buchungssystem.App.ViewModel.Base;
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
            private set
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

        private bool _occuipied;

        public bool Occupied
        {
            get => _occuipied;
            set => SetProperty(ref _occuipied, value, nameof(Occupied));
        }

        public Brush Color => (Brush) new BrushConverter().ConvertFrom(Table.Occupied ? "#f44242" : "#000000");

        // ReSharper disable once PossibleNullReferenceException : Mögliche NullreferenzExcptions wird mit Abfrage Table.Bookings.Any() vermieden
        public string LastBookingTime => Table.Bookings.Any() ? ((int) (DateTime.Now.Subtract(Table.Bookings.LastOrDefault().Created).TotalMinutes)).ToString() : "";

        #endregion

        #region Constructor

        public TableViewModel(Table table, Action<Table> onSelect, Action<Table> onStatusChanged)
        {
            _onSelect = onSelect;
            _onStatusChanged = onStatusChanged;
            Table = table;
            Occupied = Table.Occupied;

            SelectCommand = new RelayCommand(Select);
            ChangeStatusCommand = new RelayCommand(ChangeStatus);
        }

        private readonly Action<Table> _onSelect;

        private readonly Action<Table> _onStatusChanged;

        #endregion

        public ICommand SelectCommand { get; }

        private void Select()
        {
            _onSelect?.Invoke(_table);
        }

        public ICommand ChangeStatusCommand { get; }

        private void ChangeStatus()
        {
            Occupied = !Occupied;

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
            _onStatusChanged.Invoke(Table);
        }

        public void TableSelected(object sender, EventArgs e)
        {
            
        }
    }
}
