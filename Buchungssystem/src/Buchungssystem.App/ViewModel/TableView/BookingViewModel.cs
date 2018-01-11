using System;
using System.Windows.Input;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.App.ViewModel.TableView
{
    /// <summary>
    /// Kapselung einer Buchung als ViewModels
    /// </summary>
    internal class BookingViewModel : BaseViewModel
    {
        private readonly Booking _booking;

        /// <summary>
        /// Repräsentiert die Buchung
        /// </summary>
        public Booking Booking => _booking;

        public BookingViewModel(Booking booking)
        {
            _booking = booking;
        }

        /// <summary>
        /// Buchungszeit der Buchung als String
        /// </summary>
        public string BookedDate => Booking.Created.ToShortTimeString();

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        /// <param name="booking">Buchung</param>
        /// <param name="onSelect">Methode, die bei der Auswahl einer Buchung aufgerufen wird</param>
        public BookingViewModel(Booking booking, Action<BookingViewModel> onSelect)
        {
            OnSelect = onSelect;
            _booking = booking;
            SelectCommand = new RelayCommand(Select);
        }

        #region Commands

        public Action<BookingViewModel> OnSelect { get; set; }

        /// <summary>
        /// Kommando zum Auswählen einer Buchung
        /// </summary>
        public ICommand SelectCommand { get; }

        /// <summary>
        /// Ruft die Methode onSelect, welche im Konstruktor übergeben wurde, auf
        /// </summary>
        private void Select()
        {
            OnSelect?.Invoke(this);
        }



        #endregion
    }
}