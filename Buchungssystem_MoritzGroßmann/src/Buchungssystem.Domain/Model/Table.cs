using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buchungssystem.Domain.Model
{
    /// <summary>
    /// Repräsentiert einen Tisch
    /// </summary>
    public class Table : BookingSystemModel
    {
        /// <summary>
        /// Eindeutige Id des Tisches
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name des Tisches
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Offene Buchungen auf einem Tisch
        /// </summary>
        public ICollection<Booking> Bookings { get; set; }

        /// <summary>
        /// Raum des Tisches
        /// </summary>
        public Room Room { get; set; }

        /// <summary>
        /// Zeigt an, ob Tisch besetzt ist
        /// </summary>
        public bool Occupied { get; set; }

        /// <summary>
        /// Anzahl der Sitzplätze
        /// </summary>
        public int Places { get; set; }

        /// <summary>
        /// Speichert Tisch in Datenbank
        /// </summary>
        /// <returns>Gespeicherter Tisch mit Id</returns>
        public async Task<Table> Persist()
        {
            return await Task.Run(() => Persistence?.PersistTable(this));
        }

        /// <summary>
        /// Setzt den Tisch in der Datenbank auf gelöscht
        /// </summary>
        public async Task Delete()
        {
            await Task.Run(() => Persistence?.DeleteTable(this));
        }

        /// <summary>
        /// Setzt Occypied auf true und speichert dies in der Datenbank
        /// </summary>
        public async Task Occupy()
        {
            Occupied = true;
            await Task.Run(() => Persistence?.Occupy(this));
        }

        /// <summary>
        /// Setzt Occypied auf false und speichert dies in der Datenbank
        /// </summary>
        public async Task Clear()
        {
            Occupied = false;
            await Task.Run(() => Persistence?.Clear(this));
        }
    }
}