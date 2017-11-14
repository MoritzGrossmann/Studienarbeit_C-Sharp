namespace Buchungssystem.App
{
    internal class BuchungViewModel : BaseViewModel
    {
        public TischViewModel TischViewModel { get; set; }
        public Database.Buchung Buchung { get; }

        public void WaehleBuchung()
        {
            if (TischViewModel.AngewaelteBuchungen.Contains(this))
            {
                TischViewModel.AngewaelteBuchungen.Remove(this);
                TischViewModel.OffeneBuchungen.Add(this);
            }
            else
            {
                TischViewModel.OffeneBuchungen.Remove(this);
                TischViewModel.AngewaelteBuchungen.Add(this);
            }
        }

        public string Ware
        {
            get { return Buchung.Ware.Name; }
        }

        public decimal Preis
        {
            get { return Buchung.Ware.Preis; }
        }

        public BuchungViewModel(Database.Buchung buchung, TischViewModel tischViewModel)
        {
            this.Buchung = buchung;
            this.TischViewModel = tischViewModel;
        }
    }
}