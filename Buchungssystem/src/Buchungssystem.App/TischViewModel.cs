using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel
{
    internal class TischViewModel : BaseViewModel
    {
        public Tisch Tisch
        {
            get => Tisch;
            set
            {
                if (Tisch.Equals(value)) return;
                Tisch = value;
                RaisePropertyChanged(nameof(Tisch));
            }
        }

        public ObservableCollection<BuchungViewModel> OffeneBuchungen
        {
            get => OffeneBuchungen;
            set
            {
                if (OffeneBuchungen.Equals(value)) return;
                OffeneBuchungen = value;
                RaisePropertyChanged(nameof(OffeneBuchungen));
            }
        }

        public ObservableCollection<BuchungViewModel> AngewaelteBuchungen
        {
            get => AngewaelteBuchungen;
            set
            {
                if (AngewaelteBuchungen.Equals(value)) return;
                AngewaelteBuchungen = value;
                RaisePropertyChanged(nameof(AngewaelteBuchungen));
            }
        }

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
            AngewaelteBuchungen = new ObservableCollection<BuchungViewModel>();
            OffeneBuchungen = new ObservableCollection<BuchungViewModel>(new BuchungPersistenz().Buchungen(tisch).Select(b => new BuchungViewModel(b, this)));
        }
    }
}
