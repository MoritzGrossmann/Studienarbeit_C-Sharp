using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buchungssystem.Domain.Model
{
    /// <summary>
    /// Repräsentiert einen Raum
    /// </summary>
    public class Room : BookingSystemModel
    {
        /// <summary>
        /// Einfeutige Id des Raumes
        /// </summary>
        public int Id { get; set; }

        private string _name;

        /// <summary>
        /// Name des Raumes
        /// </summary>
        public string Name
        {
            get => _name;
            set { _name = value; RaisePropertyChanged(nameof(Name)); }
        }

        /// <summary>
        /// Tische in einem Raum
        /// </summary>
        public ICollection<Table> Tables { get; set; }

        /// <summary>
        /// Speichert den Raum in der Datenbank
        /// </summary>
        /// <returns>Gespeicherter Raum mit Id</returns>
        public async Task<Room> Persist()
        {
            return await Task.Run(() => Persistence?.PersistRoom(this));
        }

        /// <summary>
        /// Setzt den Raum in der Datenbank auf gelöscht
        /// </summary>
        public async Task Delete()
        {
            await Task.Run(() => Persistence.DeleteRoom(this));
        }
    }
}
 