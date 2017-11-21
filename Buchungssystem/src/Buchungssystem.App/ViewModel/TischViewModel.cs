using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Buchungssystem.App.Converter;
using Buchungssystem.App.ViewModel.Base;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;
using Unity.Interception.Utilities;

namespace Buchungssystem.App.ViewModel
{
    internal class TischViewModel : BaseViewModel
    {

        #region Properties

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

        public string Name
        {
            get => _tisch.Name;
            set
            {
                if (_tisch.Name.Equals(value)) return;
                _tisch.Name = value;
                RaisePropertyChanged(nameof(Name));
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
                RaisePropertyChanged(nameof(Belegt));
                RaisePropertyChanged(nameof(Color));
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

        private ObservableCollection<Ware> _angewaehlteWaren;
        public ObservableCollection<Ware> AngewaehlteWaren
        {
            get => _angewaehlteWaren;
            set
            {
                if (_angewaehlteWaren.Equals(value)) return;
                _angewaehlteWaren = value;
                RaisePropertyChanged(nameof(AngewaehlteWaren));
            }
        }

        public void BucheWare()
        {
            foreach (var ware in AngewaehlteWaren)
            {
                OffeneBuchungen.Add(new BuchungViewModel(new BuchungPersistenz().Buche(new Buchung() {TischId = Tisch.TischId, Ware = ware, Zeitpunkt = DateTime.Now}), this));
                AngewaehlteWaren.Remove(ware);
            }
        }

        public string VerbleibenderPreis
        {
            get
            {
                decimal sum = 0;
                _offeneBuchungen.ForEach(b => sum += b.Preis);
                var culture = CultureInfo.CurrentCulture;
                return
                    $"{decimal.Round(sum, culture.NumberFormat.CurrencyDecimalDigits, MidpointRounding.AwayFromZero)} {culture.NumberFormat.CurrencySymbol}";
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

        public string LetzteBuchung
        {
            get
            {
                if (_offeneBuchungen.Any())
                {
                    return _offeneBuchungen.Last().Buchung.Zeitpunkt.ToString();
                }
                return "Keine Buchungen";
            }
        }

        public bool Belegt => _offeneBuchungen.Any();

        public Color Color => Belegt ? Color.FromRgb(255, 230, 230) :  Color.FromRgb(204, 255, 204);

        private bool _selected;

        public bool Selected
        {
            get => _selected;
            set
            {
                if (_selected.Equals(value)) return;
                _selected = value;
                RaisePropertyChanged(nameof(Selected));
            }
        }

        #endregion

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

        #region Contructor
        public TischViewModel(Tisch tisch)
        {
            _tisch = tisch;
            _angewaehlteBuchungen = new ObservableCollection<BuchungViewModel>();
            _offeneBuchungen = new ObservableCollection<BuchungViewModel>(new BuchungPersistenz().Buchungen(tisch).Select(b => new BuchungViewModel(b, this)));
            SelectCommand = new RelayCommand(Select);
        }

        public TischViewModel()
        {
            
        }

        #endregion

        public ICommand SelectCommand;

        public void Select()
        {
            this.Selected = true;
        }
    }
}
