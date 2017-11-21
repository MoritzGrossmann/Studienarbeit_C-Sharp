using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel
{
    internal class TischViewModel : BaseViewModel
    {
        private Tisch _tisch;
        public Tisch Tisch
        {
            get => _tisch;
            set
            {
                if (_tisch.Equals(value)) return;
                _tisch = value;
                RaisePropertyChanged(nameof(Tisch));
            }
        }

        private ObservableCollection<BuchungViewModel> _offeneBuchungen;
        public ObservableCollection<BuchungViewModel> OffeneBuchungen
        {
            get => _offeneBuchungen;
            set
            {
                if (_offeneBuchungen.Equals(value)) return;
                _offeneBuchungen = value;
                RaisePropertyChanged(nameof(OffeneBuchungen));
            }
        }

        private ObservableCollection<BuchungViewModel> _angewaehlteBuchungen;
        public ObservableCollection<BuchungViewModel> AngewaelteBuchungen
        {
            get => _angewaehlteBuchungen;
            set
            {
                if (_angewaehlteBuchungen.Equals(value)) return;
                _angewaehlteBuchungen = value;
                RaisePropertyChanged(nameof(AngewaelteBuchungen));
            }
        }

        public decimal VerbleibenderPreis
        {
            get
            {
                decimal sum = 0;
                _offeneBuchungen.ForEach(b => sum += b.Preis);
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
            _angewaehlteBuchungen.ForEach(b => buchungPersistenz.Bezahle(b.Buchung));
            _angewaehlteBuchungen.Clear();
        }

        public void Storniere()
        {
            var buchungPersistenz = new BuchungPersistenz();
            _angewaehlteBuchungen.ForEach(b => buchungPersistenz.Storniere(b.Buchung));
            _angewaehlteBuchungen.Clear();
        }

        public TischViewModel(Tisch tisch)
        {
            _tisch = tisch;
            _angewaehlteBuchungen = new ObservableCollection<BuchungViewModel>();
            _offeneBuchungen = new ObservableCollection<BuchungViewModel>(new BuchungPersistenz().Buchungen(tisch).Select(b => new BuchungViewModel(b, this)));
        }
    }
}
