using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Buchungssystem.Database
{
    public class StammdatenPersistenz
    {
        #region Warengruppe

        public Warengruppe SpeichereWarengruppe(Warengruppe warengruppe)
        { 
            using (var context = new BuchungssystemEntities())
            {
                context.Warengruppe.Add(warengruppe);
                context.SaveChanges();
                return warengruppe;
            }
        }

        public List<Warengruppe> WarenGruppen()
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Warengruppe.ToList();
            }
        }

        #endregion

        #region Ware

        public Ware SpeichereWare(Ware ware)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Ware.Add(ware);
                context.SaveChanges();
                return ware;
            }
        }

        public List<Ware> Waren()
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Ware.Where(w => !w.Deleted).ToList();
            }
        }

        public List<Ware> Waren(Warengruppe warengruppe)
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Ware.Where(w => w.Warengruppe == warengruppe && !w.Deleted).ToList();
            }
        }

        public void AenderePreis(Ware ware, decimal neuerPreis)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Ware.FirstOrDefault(w => w.Id == ware.Id).Preis = neuerPreis;
                context.SaveChanges();
            }
        }

        public void LoescheWare(Ware ware)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Ware.FirstOrDefault(w => w.Id == ware.Id).Deleted = true;
                context.SaveChanges();
            }
        }

        #endregion

        #region Raum

        public List<Raum> Raeume()
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Raum.ToList();
            }
        }

        public Raum SpeichereRaum(Raum raum)
        {
            using (var context = new BuchungssystemEntities())
            { 
                context.Raum.Add(raum);
                context.SaveChanges();
                return raum;
            }
        }

        public void LoescheRaum(Raum raum)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Raum.Remove(raum);
                context.SaveChanges();
            }
        }

        #endregion

        #region Tisch

        public Tisch SpeichereTisch(Tisch tisch)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Tisch.Add(tisch);
                context.SaveChanges();
                return tisch;
            }
        }

        public void LoescheTisch(Tisch tisch)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Tisch.Remove(tisch);
                context.SaveChanges();
            }
        }

        public List<Tisch> Tische()
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Tisch.ToList();
            }
        }

        public List<Tisch> Tische(Raum raum)
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Tisch.Where(t => t.Raum == raum).ToList();
            }
        }

        #endregion
    }
}
