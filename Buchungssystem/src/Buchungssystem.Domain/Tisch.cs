namespace Buchungssystem.Domain
{
    public class Tisch
    {
        public int TischId { get; set; }

        public int Nummer { get; set; }

        public int Sitzplaetze { get; set; }

        public int RaumId { get; set; }

        public virtual Raum Raum { get; set; }
    }
}