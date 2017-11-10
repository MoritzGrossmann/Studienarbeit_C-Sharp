namespace Buchungssystem.Domain.Interfaces
{
    public interface IchSpeichereStammdaten
    {
        void AenderePreis(Domain.Ware ware, decimal neuerPreis);

        Warengruppe SpeichereWarengruppe(Domain.Warengruppe warengruppe);

        Ware SpeichereWare(Domain.Ware ware);

        Raum SpeichereRaum(Domain.Raum raum);

        Tisch SpeichereTisch(Domain.Tisch tisch);

    }
}
