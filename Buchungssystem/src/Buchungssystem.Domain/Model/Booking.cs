using System;

namespace Buchungssystem.Domain.Model
{
    /// <summary>
    /// Repräsentiert eine Buchung
    /// </summary>
    public class Booking : BookingSystemModel
    {
        /// <summary>
        /// Eindeutige ID der Buchung
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Zeitpunkt der Buchung
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Zeitpunkt, wann die Buchung Bezahlt oder Stoerniert wurde
        /// </summary>
        public DateTime Finished { get; set; }

        /// <summary>
        /// Produkt, welches gebucht wurde
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Tisch der Buchung
        /// </summary>
        public Table Table { get; set; }

        /// <summary>
        /// Betrag der Buchung
        /// </summary>
        public decimal Price { get;  set; }

        /// <summary>
        /// Buchungsstatus
        /// </summary>
        public BookingStatus Status { get; set; }

        /// <summary>
        /// Speichert die Buchung in der Datenbank
        /// </summary>
        /// <returns></returns>
        public Booking Persist()
        {
            Status = BookingStatus.Open;
            Price = Product.Price;
            Created = DateTime.Now;
            Finished = DateTime.Now;
            return Persistence?.Book(this);
        }

        /// <summary>
        /// Setzt den Status der Buchung auf Bezahlt
        /// </summary>
        public void Pay()
        {
            Finished = DateTime.Now;
            Persistence?.Pay(this);
            Status = BookingStatus.Paid;
        }

        /// <summary>
        /// Setzt den Status der Buchung auf Stoerniert
        /// </summary>
        public void Cancel()
        {
            Finished = DateTime.Now;
            Persistence?.Cancel(this);
            Status = BookingStatus.Paid;

            new Booking() { Finished = DateTime.Now, Product = Product, Table = Table, Status = BookingStatus.Cancled, Persistence = Persistence}.Persist();
        }
    }
}