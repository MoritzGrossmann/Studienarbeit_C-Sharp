using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Buchungssystem.Database
{
    public class BuchungPersistenz
    {
        public Buchung Buche(Buchung buchung)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Buchung.Add(buchung);
                context.SaveChanges();
                return buchung;
            }
        }

        public void Storniere(Buchung buchung)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Buchung.FirstOrDefault(b => b.Id == buchung.Id).Status =
                    (int) BuchungsStatus.Storiert;
                context.SaveChanges();
            }
        }

        public void Bezahle(Buchung buchung)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Buchung.FirstOrDefault(b => b.Id == buchung.Id).Status =
                    (int)BuchungsStatus.Bezahlt;
                context.SaveChanges();
            }
        }

        public List<Buchung> Buchungen(Tisch tisch)
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Buchung.Where(b => b.TischId == tisch.Id).ToList();
            }
        }

        public List<Buchung> Buchungen(DateTime dateTime)
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Buchung.Where(b => b.Zeitpunkt.Date == dateTime.Date).ToList();
            }
        }

        public List<Buchung> Buchungen(Tisch tisch, BuchungsStatus status)
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Buchung.Where(b => b.TischId == tisch.Id && b.Status == (int) status).ToList();
            }
        }
    }
}
