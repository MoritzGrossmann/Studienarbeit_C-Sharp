using System;
using System.Collections.Generic;
using System.Linq;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Repository
{
    public class BuchungPersistenz
    {
        public Buchung Buche(Buchung buchung)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Buchungen.Add(buchung);
                context.SaveChanges();
                return buchung;
            }
        }

        public void Storniere(Buchung buchung)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Buchungen.FirstOrDefault(b => b.BuchungId == buchung.BuchungId).Status =
                    (int) BuchungsStatus.Storiert;
                context.SaveChanges();
            }
        }

        public void Bezahle(Buchung buchung)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Buchungen.FirstOrDefault(b => b.BuchungId == buchung.BuchungId).Status =
                    (int)BuchungsStatus.Bezahlt;
                context.SaveChanges();
            }
        }

        public List<Buchung> Buchungen(Tisch tisch)
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Buchungen.Where(b => b.TischId == tisch.TischId).ToList();
            }
        }

        public List<Buchung> Buchungen(DateTime dateTime)
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Buchungen.Where(b => b.Zeitpunkt.Date == dateTime.Date).ToList();
            }
        }

        public List<Buchung> Buchungen(Tisch tisch, BuchungsStatus status)
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Buchungen.Where(b => b.TischId == tisch.TischId && b.Status == (int) status).ToList();
            }
        }
    }
}
