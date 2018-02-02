using System;
using System.Threading.Tasks;

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
        public async Task<Booking> Persist()
        {
            Status = BookingStatus.Open;
            Price = Product.Price;
            Created = DateTime.Now;
            Finished = DateTime.Now;
            return await Task.Run(() => Persistence?.Book(this));
        }

        /// <summary>
        /// Setzt den Status der Buchung auf Bezahlt
        /// </summary>
        public async Task Pay()
        {
            await Task.Run(() =>
            {
                Finished = DateTime.Now;
                Persistence?.Pay(this);
                Status = BookingStatus.Paid;
            });
        }

        /// <summary>
        /// Setzt den Status der Buchung auf Stoerniert
        /// </summary>
        public async Task Cancel()
        {
            await Task.Run(() =>
            {
                Finished = DateTime.Now;
                Persistence?.Cancel(this);
                Status = BookingStatus.Cancled;
            });
        }
    }
}