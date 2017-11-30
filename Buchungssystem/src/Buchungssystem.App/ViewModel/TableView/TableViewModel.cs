﻿using System;
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
        private readonly IPersistBaseData _baseDataPersistence;

        private readonly IPersistBooking _bookingPersistence;

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

        private ObservableCollection<BookingViewModel> _openBookings;
        public ObservableCollection<BookingViewModel> OpenBookings
        {
            get => _openBookings;
            set
            {
                if (_openBookings.Equals(value)) return;
                _openBookings = value;
                RaisePropertyChanged(nameof(OpenBookings));
                RaisePropertyChanged(nameof(InUse));
                RaisePropertyChanged(nameof(Color));
            }
        }

        public string VerbleibenderPreis
        {
            get
            {
                decimal sum = 0;
                _openBookings.ForEach(b => sum += b.Booking.Product.Price);
                var culture = CultureInfo.CurrentCulture;
                return
                    $"{decimal.Round(sum, culture.NumberFormat.CurrencyDecimalDigits, MidpointRounding.AwayFromZero)} {culture.NumberFormat.CurrencySymbol}";
            }
        }

        public string LastBooking
        {
            get
            {
                if (_openBookings.Any())
                {
                    return
                        $"Letzte Buchung {_openBookings.Last().Booking.Timestamp.ToString(CultureInfo.CurrentCulture)}";
                }
                return "Keine Buchungen";
            }
        }

        public bool InUse => _table.Occupied;

        public Brush Color => 
            InUse ? (Brush)new BrushConverter().ConvertFrom("#FFE6E6") : OpenBookings.Any() ? (Brush)new BrushConverter().ConvertFrom("#f7ffb7") : (Brush)new BrushConverter().ConvertFrom("#CCFFCC");

        private bool _selected;

        public bool Selected
        {
            get => _selected;
            set
            {
                if (_selected.Equals(value)) return;
                _selected = value;
                RaisePropertyChanged(nameof(Selected));
            }
        }

        public string ChangeStatusTest => InUse ? "Tisch abräumen" : "Tisch besetzen";

        #endregion

        #region Contructor

        public TableViewModel(IPersistBaseData baseDataPersistence, IPersistBooking bookingPersistence, Table table,
            Action<Table> onTableSelected)
        {
            _baseDataPersistence = baseDataPersistence;
            _bookingPersistence = bookingPersistence;

            _onTableSelected = onTableSelected;
            _table = table;

            _openBookings = new ObservableCollection<BookingViewModel>(
                _bookingPersistence.Bookings(_table, BookingStatus.Open)
                    .Select(booking => new BookingViewModel(booking)));

            SelectCommand = new RelayCommand(Select);
            ChangeStatusCommand = new RelayCommand(ChangeStatus);

        }

        public TableViewModel(IPersistBaseData baseDataPersistence, Table table, Action<Table> onSelect)
        {
            _baseDataPersistence = baseDataPersistence;
            _onTableSelected = onSelect;
            _table = table;

            SelectCommand = new RelayCommand(Select);
        }

        private readonly Action<Table> _onTableSelected;
        public TableViewModel()
        {
            
        }

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
                _bookingPersistence.Clear(Table);
            }
            else
            {
                _bookingPersistence.Occupy(Table);
            }

            Table.Occupied = !Table.Occupied;
            RaisePropertyChanged(nameof(InUse));
            RaisePropertyChanged(nameof(ChangeStatusTest));
            RaisePropertyChanged(nameof(Color));
        }
    }
}