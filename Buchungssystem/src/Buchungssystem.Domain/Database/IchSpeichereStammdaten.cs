using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buchungssystem.Domain.Database
{
    public interface IchSpeichereStammdaten
    {
        void AenderePreis(Ware ware, decimal neuerPreis);

        void SpeichereWarengruppe(Warengruppe warengruppe);

        void SpeichereWare(Ware ware);

        void SpeichereRaum(Raum raum);

        void SpeichereTisch(Tisch tisch);

    }
}
