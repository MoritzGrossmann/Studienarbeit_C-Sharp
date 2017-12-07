using System;
using System.Data.Entity;
using System.Linq;
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
                Id = 1,
                Name = "Getränke",
                ParentId = 1
            });

            context.ProductGroups.Add(new DbProductGroup()
            {
                ParentId = 1,
                Name = "Longdrinks",
                Id = 6
            });

            context.ProductGroups.Add(new DbProductGroup()
            {
                ParentId = 1,
                Name = "Cocktails",
                Id = 2
            });

            context.ProductGroups.Add(new DbProductGroup()
            {
                ParentId = 1,
                Name = "Vodka",
                Id = 3
            });

            context.ProductGroups.Add(new DbProductGroup()
            {
                ParentId = 1,
                Name = "Rum",
                Id = 4
            });

            context.ProductGroups.Add(new DbProductGroup()
            {
                ParentId = 1,
                Name = "Gin",
                Id = 5
            });

            context.SaveChanges();

            context.Products.Add(new DbProduct()
            {
                Name = "Gin Tonic",
                Price = (decimal)5.5,
                DbProductGroupId = 6
            });
            context.Products.Add(new DbProduct()
            {
                Name = "Whiskey Cola",
                Price = (decimal)6,
                DbProductGroupId = 6
            });
            context.Products.Add(new DbProduct()
            {
                Name = "Wodka Lemon",
                Price = (decimal)5.5,
                DbProductGroupId = 6
            });
            context.Products.Add(new DbProduct()
            {
                Name = "Wodka Orange",
                Price = (decimal)5.5,
                DbProductGroupId = 6
            });
            context.Products.Add(new DbProduct()
            {
                Name = "Campari Orange",
                Price = (decimal)5.5,
                DbProductGroupId = 6
            });
            context.Products.Add(new DbProduct()
            {
                Name = "Gin Lemon",
                Price = (decimal)5.5,
                DbProductGroupId = 6
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
                    RoomId = 1,
                    Name = $"Terrasse {i}"
                });

                context.Tables.Add(new DbTable()
                {
                    Places = new Random().Next(2, 8),
                    RoomId = 2,
                    Name = $"Tisch {i}"

                });
            }

            context.SaveChanges();

            for (int i = 0; i < 20; i++)
            {
                DbProduct product = context.Products.ToList().ElementAt(new Random().Next(1, 5));
                context.Bookings.Add(new DbBooking()
                {

                    Status = (int)BookingStatus.Open,
                    TableId = new Random().Next(1, 10),
                    ProductId = product.DbProductId,
                    Price = product.Price,
                    Created = DateTime.Now
                });
            }

            base.Seed(context);
        }
    }

}
