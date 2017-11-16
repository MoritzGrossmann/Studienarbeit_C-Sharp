using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buchungssystem.App.ViewModel;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;

namespace Buchungssystem.App
{
    internal class RaumViewModel : BaseViewModel
    {
        public StammdatenPersistenz StammdatenPersistenz = new StammdatenPersistenz();
        public Raum Raum { get; }
        public List<TischViewModel> Tische { get; }
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
            Tische = StammdatenPersistenz.Tische(raum);
        }
    }
}
