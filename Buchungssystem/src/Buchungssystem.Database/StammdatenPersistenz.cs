using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain;
using Buchungssystem.Domain.Database;

namespace Buchungssystem.Database
{
    public class StammdatenPersistenz : IchSpeichereStammdaten
    {
        public void AenderePreis(Ware ware, decimal neuerPreis)
        {
            using (var context = new BuchungssystemEntities())
            {
                ware.Preis = neuerPreis;
                context.SaveChanges();
            }
        }

        public void SpeichereWarengruppe(Warengruppe warengruppe)
        { 
            using (var context = new BuchungssystemEntities())
            {
                if (context.Warengruppen.Count(w => w.WarengruppenId == warengruppe.WarengruppenId) > 0)
                {
                    var entity =
                        context.Warengruppen.FirstOrDefault(w => w.WarengruppenId == warengruppe.WarengruppenId);
                    if (entity != null) entity.Name = warengruppe.Name;
                }
                else
                {
                    context.Warengruppen.Add(warengruppe);
                }
                context.SaveChanges();
            }
        }

        public void SpeichereWare(Ware ware)
        {
            throw new NotImplementedException();
        }

        public void SpeichereRaum(Raum raum)
        {
            using (var context = new BuchungssystemEntities())
            {
                if (context.Raeume.Count(r => r.RaumId == raum.RaumId) > 0)
                {
                    var entity =
                        context.Raeume.FirstOrDefault(r => r.RaumId == raum.RaumId);
                    if (entity != null) entity.Name = raum.Name;
                }
                else
                {
                    context.Raeume.Add(raum);
                }
                context.SaveChanges();
            }
        }

        public void SpeichereTisch(Tisch tisch)
        {
            throw new NotImplementedException();
        }
    }
}
