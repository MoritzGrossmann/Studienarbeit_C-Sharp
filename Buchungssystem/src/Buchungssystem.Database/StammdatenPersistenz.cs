﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Database
{
    public class StammdatenPersistenz
    {
        #region Warengruppe

        public Task<Warengruppe> SpeichereWarengruppe(Warengruppe warengruppe)
        {
            return Task.Run(() => SpeichereWarengruppeInDatenbank(warengruppe));
        }

        private Warengruppe SpeichereWarengruppeInDatenbank(Warengruppe warengruppe)
        { 
            using (var context = new BuchungssystemEntities())
            {
                context.Warengruppen.Add(warengruppe);
                context.SaveChanges();
                return warengruppe;
            }
        }

        public Task<List<Warengruppe>> WarenGruppen()
        {
            using (var context = new BuchungssystemEntities())
            {
                return Task.Run(() => context.Warengruppen.ToList());
            }
        }

        #endregion

        #region Ware

        public Ware SpeichereWare(Ware ware)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Waren.Add(ware);
                context.SaveChanges();
                return ware;
            }
        }

        public Task<List<Ware>> Waren()
        {
            using (var context = new BuchungssystemEntities())
            {
                return Task.Run(() => context.Waren.Where(w => !w.Deleted).ToList());
            }
        }

        public Task<List<Ware>> Waren(Warengruppe warengruppe)
        {
            using (var context = new BuchungssystemEntities())
            {
                return Task.Run(() => context.Waren.Where(w => w.Warengruppe == warengruppe && !w.Deleted).ToList());
            }
        }

        public void AenderePreis(Ware ware, decimal neuerPreis)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Waren.FirstOrDefault(w => w.WarenId == ware.WarenId).Preis = neuerPreis;
                context.SaveChanges();
            }
        }

        public void LoescheWare(Ware ware)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Waren.FirstOrDefault(w => w.WarenId == ware.WarenId).Deleted = true;
                context.SaveChanges();
            }
        }

        #endregion

        #region Raum

        public List<Raum> Raeume()
        {
            using (var context = new BuchungssystemEntities())
            {
                return context.Raeume.ToList();
            }
        }

        public Raum SpeichereRaum(Raum raum)
        {
            using (var context = new BuchungssystemEntities())
            { 
                context.Raeume.Add(raum);
                context.SaveChanges();
                return raum;
            }
        }

        public void LoescheRaum(Raum raum)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Raeume.Remove(raum);
                context.SaveChanges();
            }
        }

        #endregion

        #region Tisch

        public Tisch SpeichereTisch(Tisch tisch)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Tische.Add(tisch);
                context.SaveChanges();
                return tisch;
            }
        }

        public void LoescheTisch(Tisch tisch)
        {
            using (var context = new BuchungssystemEntities())
            {
                context.Tische.Remove(tisch);
                context.SaveChanges();
            }
        }

        public Task<List<Tisch>> Tische()
        {
            using (var context = new BuchungssystemEntities())
            {
                return Task.Run(() => context.Tische.ToList());
            }
        }

        public Task<List<Tisch>> Tische(Raum raum)
        {
            using (var context = new BuchungssystemEntities())
            {
                return Task.Run(() => context.Tische.Where(t => t.Raum == raum).ToList());
            }
        }

        #endregion
    }
}
