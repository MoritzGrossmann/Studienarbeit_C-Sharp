using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buchungssystem.Domain.Database
{
    public interface IchLadeBuchungsdaten
    {
        List<Buchung> LadeBuchungenVonTisch(Tisch tisch);
    }
}
