using System;
using System.Data.Entity;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository.Model;

namespace Buchungssystem.Repository
{
    public class BookingsystemEntities : DbContext
    {
        static BookingsystemEntities() 
        {
            // Not initialize database
            //  Database.SetInitializer<ProjectDatabase>(null);
            // Database initialize

            System.Data.Entity.Database.SetInitializer<BookingsystemEntities>(new DbInitializer());
            using (BookingsystemEntities db = new BookingsystemEntities())
                db.Database.Initialize(false);

            //var context = new BookingsystemEntities();
            //context.Database.Create();
        }

        public DbSet<DbRoom> Rooms { get; set; }
        public DbSet<DbTable> Tables { get; set; }
        public DbSet<DbBooking> Bookings { get; set; }
        public DbSet<DbProduct> Products { get; set; }
        public DbSet<DbProductGroup> ProductGroups { get; set; }

    }

    class DbInitializer : DropCreateDatabaseIfModelChanges<BookingsystemEntities>
    {
        protected override void Seed(BookingsystemEntities context)
        {
            context.ProductGroups.Add(new DbProductGroup()
            {
                Name = "Longdrinks"
            });

            context.ProductGroups.Add(new DbProductGroup()
            {
                Name = "Cocktails"
            });

            context.ProductGroups.Add(new DbProductGroup()
            {
                Name = "Vodka"
            });

            context.ProductGroups.Add(new DbProductGroup()
            {
                Name = "Rum"
            });

            context.ProductGroups.Add(new DbProductGroup()
            {
                Name = "Gin"
            });

            context.SaveChanges();

            context.Products.Add(new DbProduct()
            {
                Name = "Gin Tonic",
                Price = (decimal)5.5,
                DbProductGroupId = 1
            });
            context.Products.Add(new DbProduct()
            {
                Name = "Whiskey Cola",
                Price = (decimal)6,
                DbProductGroupId = 1
            });
            context.Products.Add(new DbProduct()
            {
                Name = "Wodka Lemon",
                Price = (decimal)5.5,
                DbProductGroupId = 1
            });
            context.Products.Add(new DbProduct()
            {
                Name = "Wodka Orange",
                Price = (decimal)5.5,
                DbProductGroupId = 1
            });
            context.Products.Add(new DbProduct()
            {
                Name = "Campari Orange",
                Price = (decimal)5.5,
                DbProductGroupId = 1
            });
            context.Products.Add(new DbProduct()
            {
                Name = "Gin Lemon",
                Price = (decimal)5.5,
                DbProductGroupId = 1
            });

            context.Rooms.Add(new DbRoom()
            {
                Name = "Terrasse",
            });

            context.Rooms.Add(new DbRoom()
            {
                Name = "Saal",
            });

            for (int i = 1; i <= 20; i++)
            {
                context.Tables.Add(new DbTable()
                {
                    Places = new Random().Next(2,4),
                    DbRoomId = 1,
                    Name = $"Terrasse {i}"
                });

                context.Tables.Add(new DbTable()
                {
                    Places = new Random().Next(2, 8),
                    DbRoomId = 2,
                    Name = $"Tisch {i}"

                });
            }

            context.SaveChanges();

            for (int i = 0; i < 20; i++)
            {
                context.Bookings.Add(new DbBooking()
                {
                    Status = (int)BookingStatus.Open,
                    DbTableId = new Random().Next(1, 10),
                    DbProductId = new Random().Next(1, 5)
                });
            }

            base.Seed(context);
        }
    }

}
