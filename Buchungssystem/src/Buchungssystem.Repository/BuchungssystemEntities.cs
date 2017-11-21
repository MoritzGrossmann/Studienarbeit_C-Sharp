using System;
using System.Data.Entity;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Repository
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

            //var context = new BuchungssystemEntities();
            //context.Database.Create();
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
            //context.Warengruppen.Add(new Warengruppe()
            //{
            //    Name = "Longdrinks"
            //});

            //context.Waren.Add(new Ware()
            //{
            //    Name = "Gin Tonic",
            //    Preis = (decimal)5.5,
            //    WarengruppenId = 1
            //});
            //context.Waren.Add(new Ware()
            //{
            //    Name = "Whiskey Cola",
            //    Preis = (decimal)6,
            //    WarengruppenId = 1
            //});
            //context.Waren.Add(new Ware()
            //{
            //    Name = "Wodka Lemon",
            //    Preis = (decimal)5.5,
            //    WarengruppenId = 1
            //});
            //context.Waren.Add(new Ware()
            //{
            //    Name = "Wodka Orange",
            //    Preis = (decimal)5.5,
            //    WarengruppenId = 1
            //});
            //context.Waren.Add(new Ware()
            //{
            //    Name = "Campari Orange",
            //    Preis = (decimal)5.5,
            //    WarengruppenId = 1
            //});
            //context.Waren.Add(new Ware()
            //{
            //    Name = "Gin Lemon",
            //    Preis = (decimal)5.5,
            //    WarengruppenId = 1
            //});

            context.Raeume.Add(new Raum()
            {
                Name = "Terrasse",
            });

            context.Raeume.Add(new Raum()
            {
                Name = "Saal",
            });

            for (int i = 1; i <= 20; i++)
            {
                context.Tische.Add(new Tisch()
                {
                    Plaetze = new Random().Next(2,4),
                    RaumId = 1,
                    Name = $"Terrasse {i}"
                });

                context.Tische.Add(new Tisch()
                {
                    Plaetze = new Random().Next(2, 8),
                    RaumId = 2,
                    Name = $"Tisch {i}"

                });
            }

            //for (int i = 0; i < 20; i++)
            //{
            //    context.Buchungen.Add(new Buchung()
            //    {
            //        Status = (int) BuchungsStatus.Offen,
            //        TischId = new Random().Next(1, 10),
            //        WarenId = new Random().Next(1, 5),
            //        Zeitpunkt = DateTime.Now
            //    });
            //}

            base.Seed(context);
        }
    }

}
