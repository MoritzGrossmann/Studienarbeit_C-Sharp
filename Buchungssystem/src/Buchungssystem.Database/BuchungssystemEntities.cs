﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Database
{
    public class BuchungssystemEntities : DbContext
    {
        static BuchungssystemEntities() 
        {
            // Not initialize database
            //  Database.SetInitializer<ProjectDatabase>(null);
            // Database initialize
            System.Data.Entity.Database.SetInitializer<BuchungssystemEntities>(new DbInitializer());
            using (BuchungssystemEntities db = new BuchungssystemEntities())
                db.Database.Initialize(false);
        }

        public DbSet<Raum> Raeume { get; set; }
        public DbSet<Tisch> Tische { get; set; }
        public DbSet<Buchung> Buchungen { get; set; }
        public DbSet<Ware> Waren { get; set; }
        public DbSet<Warengruppe> Warengruppen { get; set; }

    }

    class DbInitializer : DropCreateDatabaseAlways<BuchungssystemEntities>
    {
        protected override void Seed(BuchungssystemEntities context)
        {
            context.Raeume.Add(new Raum()
            {
                Name = "Terrasse",
                RaumId = 1
            });

            base.Seed(context);
        }
    }

}
