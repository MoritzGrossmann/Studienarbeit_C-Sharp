namespace Buchungssystem.Domain.Interfaces
{
    public interface IchSpeichereBuchungsdaten
    {
        Buche(Buchung buchung);

        void Storniere(Buchung buchung);

        void Bezahle(Buchung buchung);
    }
}
