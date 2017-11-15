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
            context.Raeume.Add(new Raum()
            {
                Name = "Terrasse",
            });

            context.Raeume.Add(new Raum()
            {
                Name = "Saal",
            });

            context.Tische.Add(new Tisch()
            {

                Plaetze = 4,
                RaumId = 2,
                Name = "Tisch 1"
            });

            context.Tische.Add(new Tisch()
            { 
                Plaetze = 4,
                RaumId = 2,
                Name = "Tisch 2"
            });

            base.Seed(context);
        }
    }

}
