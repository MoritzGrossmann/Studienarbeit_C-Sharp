using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;

namespace Buchungssystem.App
{
    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Raeume = StammdatenPersistenz.Raeume();
            AktiverRaum = new RaumViewModel(Raeume.First());
        }
 
        public StammdatenPersistenz StammdatenPersistenz = new StammdatenPersistenz();
        public List<Raum> Raeume { get; }

        public RaumViewModel AktiverRaum { get; set; }
    }
}
