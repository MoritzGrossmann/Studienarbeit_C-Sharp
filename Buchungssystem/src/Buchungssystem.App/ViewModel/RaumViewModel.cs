using Buchungssystem.Database;

namespace Buchungssystem.App.ViewModel
{
    class RaumViewModel : BaseViewModel
    {
        public Raum Raum { get; }

        public string Name
        {
            get => Raum.Name;
            set
            {
                if (!Equals(Raum.Name, value))
                {
                    Raum.Name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }
        public RaumViewModel(Raum raum)
        {
            Raum = raum;
        }
    }
}
