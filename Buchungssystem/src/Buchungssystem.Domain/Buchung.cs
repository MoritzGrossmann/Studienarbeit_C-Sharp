using System;

namespace Buchungssystem.Domain
{
    public class Buchung
    {
        public int BuchungsId { get; set; }
        public int TischId { get; set; }
        public virtual Tisch Tisch { get; set; }
        public int WarenId { get; set; }
        public virtual Ware Ware { get; set; }
        public DateTime BuchungsZeitpunkt { get; set; }
        public int BuchungsStatusId { get; set; }
        public virtual BuchungsStatus Status { get; set; }
    }
}