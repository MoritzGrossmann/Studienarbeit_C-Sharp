using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel.TableView
{
    /// <summary>
    /// Repräsentiert einen Tisch
    /// </summary>
    internal class TableViewModel : BaseViewModel
    {
        #region Properties

        private Table _table;

        /// <summary>
        /// Gekapselter Tisch
        /// </summary>
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

        /// <summary>
        /// Name des Tisches
        /// </summary>
        public string Name => _table.Name;

        /// <summary>
        /// Summe der Preise der gebuchten Waren auf dem Tisch
        /// </summary>
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

        /// <summary>
        /// Zeigt an, ob der Tisch besetzt ist
        /// </summary>
        public bool Occupied
        {
            get => _occuipied;
            set => SetProperty(ref _occuipied, value, nameof(Occupied));
        }

        // ReSharper disable once PossibleNullReferenceException : Mögliche NullreferenzExcptions wird mit Abfrage Table.Bookings.Any() vermieden

        /// <summary>
        /// Zeit im Minuten als String die vergangen ist, seitdem das letzte Mal auf den Tisch gebucht wurde
        /// </summary>
        public string LastBookingTime => Table.Bookings.Any() ? ((int) (DateTime.Now.Subtract(Table.Bookings.LastOrDefault().Created).TotalMinutes)).ToString() : "";

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table">Tisch, welcher das TableViewModel repräsentiert</param>
        /// <param name="onSelect">Methode, die ausgeführt wird, wenn der Tisch ausgewählt wurde</param>
        /// <param name="onStatusChanged">Methode, die ausgeführt wird, wenn der status des Tisches geändert wurde</param>
        public TableViewModel(Table table, Action<Table> onSelect, Action<Table> onStatusChanged)
        {
            _onSelect = onSelect;
            _onStatusChanged = onStatusChanged;
            Table = table;
            Occupied = Table.Occupied;

            SelectCommand = new RelayCommand(Select);
            ChangeStatusCommand = new RelayCommand(ChangeStatus);
        }



        #endregion

        #region Commands

        /// <summary>
        /// Kommando zum Auswählen des Tisches
        /// </summary>
        public ICommand SelectCommand { get; }

        /// <summary>
        /// Kommndo zum Ändern des Stati des Tisches
        /// </summary>
        public ICommand ChangeStatusCommand { get; }

        #endregion

        #region Actions

        /// <summary>
        /// Ruft die im Kontriktor übergebene Methode onSelect auf und übergibt den Tisch
        /// </summary>
        private void Select()
        {
            _onSelect?.Invoke(_table);
        }

        /// <summary>
        /// Ändert den Status des Tisches von Frei auf besetzt oder umgekehrt
        /// Ruft die im Konstruktor übergebene Methode onStatusChanged auf
        /// </summary>
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

        private readonly Action<Table> _onSelect;

        private readonly Action<Table> _onStatusChanged;

        #endregion
    }
}
