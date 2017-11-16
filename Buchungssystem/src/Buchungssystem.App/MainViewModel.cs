using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository;

namespace Buchungssystem.App
{
    internal class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Raeume = StammdatenPersistenz.Raeume().ToList().Select(r => new RaumViewModel(r)).ToList();
        }

        private IchPersistiereStammdaten StammdatenPersistenz;

        public List<RaumViewModel> Raeume { get; }
    }
}
