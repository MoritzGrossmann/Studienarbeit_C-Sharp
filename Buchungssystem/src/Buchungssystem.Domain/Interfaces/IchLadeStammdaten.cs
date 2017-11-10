using System.Collections.Generic;

namespace Buchungssystem.Domain.Interfaces
{
    public interface IchLadeStammdaten
    {
        List<Raum> Raeume();

        List<Tisch> Tische(Raum raum);

        List<Warengruppe> WarenGruppen();

        List<Ware> Waren();

        List<Ware> Waren(Warengruppe warengruppe);
    }
}
