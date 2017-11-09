using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.Database
{
    public class BuchungPersistenz : IchSpeichereBuchungsdaten, IchLadeBuchungsdaten
    {
        public void Buche(Buchung buchung)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Buchungen.Add(buchung);
                context.SaveChanges();
            }
        }

        public void Storniere(Buchung buchung)
        {
            using (var context = new BuchungssystemEntities())
            {
                try
                {
                    context.Buchungen.FirstOrDefault(b => b.BuchungsId == buchung.BuchungsId).BuchungsStatus =
                        (int) BuchungsStatus.Storiert;
                    context.SaveChanges();
                }
                catch (NullReferenceException)
                {
                    
                }
            }
        }

        public void Bezahle(Buchung buchung)
        {
            using (var context = new BuchungssystemEntities())
            {
                try
                {
                    context.Buchungen.FirstOrDefault(b => b.BuchungsId == buchung.BuchungsId).BuchungsStatus =
                        (int)BuchungsStatus.Bezahlt;
                    context.SaveChanges();
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        public List<Buchung> LadeBuchungenVonTisch(Tisch tisch)
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Buchungen.Where(b => b.Tisch == tisch).ToList();
            }
        }
    }
}
