using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buchungssystem.Domain.Database
{
    public interface IchSpeichereBuchungsdaten
    {
        void Buche(Buchung buchung);

        void Storniere(Buchung buchung);

        void Bezahle(Buchung buchung);
    }
}
