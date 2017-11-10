using System.Collections.Generic;

namespace Buchungssystem.Domain.Interfaces
{
    public interface IchLadeBuchungsdaten
    {
        List<Database.> LadeBuchungenVonTisch(Tisch tisch);
    }
}
