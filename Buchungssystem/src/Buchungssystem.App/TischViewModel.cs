using System.Collections.Generic;
using System.Linq;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;

namespace Buchungssystem.App.ViewModel
{
    internal class TischViewModel
    {
        public Tisch Tisch{get;}

        public List<BuchungViewModel> OffeneBuchungen { get; set; }

        public List<BuchungViewModel> AngewaelteBuchungen { get; }

        public decimal VerbleibenderPreis
        {
            get
            {
                decimal sum = 0;
                OffeneBuchungen.ForEach(b => sum += b.Preis);
                return sum;
            }
        }

        public decimal AngewahltePreis
        {
            get
            {
                decimal sum = 0;
                AngewaelteBuchungen.ForEach(b => sum += b.Preis);
                return sum;
            }
        }

        public void Bezahle()
        {
            var buchungPersistenz = new BuchungPersistenz();
            AngewaelteBuchungen.ForEach(b => buchungPersistenz.Bezahle(b.Buchung));
            AngewaelteBuchungen.Clear();
        }

        public void Storniere()
        {
            var buchungPersistenz = new BuchungPersistenz();
            AngewaelteBuchungen.ForEach(b => buchungPersistenz.Storniere(b.Buchung));
            AngewaelteBuchungen.Clear();
        }

        public TischViewModel(Tisch tisch)
        {
            Tisch = tisch;
            AngewaelteBuchungen = new List<BuchungViewModel>();
            OffeneBuchungen = new BuchungPersistenz().Buchungen(tisch).Select(b => new BuchungViewModel(b, this)).ToList();
        }
    }
}
