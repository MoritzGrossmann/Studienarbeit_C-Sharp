using System;
using System.Data.Entity;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Repository
{
    public class BookingsystemEntities : DbContext
    {
        static BookingsystemEntities() 
        {
            // Not initialize database
            //  Database.SetInitializer<ProjectDatabase>(null);
            // Database initialize

            //System.Data.Entity.Database.SetInitializer<BookingsystemEntities>(new DbInitializer());
            //using (BookingsystemEntities db = new BookingsystemEntities())
            //    db.Database.Initialize(false);

            //var context = new BookingsystemEntities();
            //context.Database.Create();
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }

    }

    class DbInitializer : DropCreateDatabaseAlways<BookingsystemEntities>
    {
        protected override void Seed(BookingsystemEntities context)
        {
            //context.ProductGroups.Add(new ProductGroup()
            //{
            //    Name = "Longdrinks"
            //});

            //context.Products.Add(new Product()
            //{
            //    Name = "Gin Tonic",
            //    Price = (decimal)5.5,
            //    ProductGroupId = 1
            //});
            //context.Products.Add(new Product()
            //{
            //    Name = "Whiskey Cola",
            //    Price = (decimal)6,
            //    ProductGroupId = 1
            //});
            //context.Products.Add(new Product()
            //{
            //    Name = "Wodka Lemon",
            //    Price = (decimal)5.5,
            //    ProductGroupId = 1
            //});
            //context.Products.Add(new Product()
            //{
            //    Name = "Wodka Orange",
            //    Price = (decimal)5.5,
            //    ProductGroupId = 1
            //});
            //context.Products.Add(new Product()
            //{
            //    Name = "Campari Orange",
            //    Price = (decimal)5.5,
            //    ProductGroupId = 1
            //});
            //context.Products.Add(new Product()
            //{
            //    Name = "Gin Lemon",
            //    Price = (decimal)5.5,
            //    ProductGroupId = 1
            //});

            context.Rooms.Add(new Room()
            {
                Name = "Terrasse",
            });

            context.Rooms.Add(new Room()
            {
                Name = "Saal",
            });

            for (int i = 1; i <= 20; i++)
            {
                context.Tables.Add(new Table()
                {
                    Places = new Random().Next(2,4),
                    RoomId = 1,
                    Name = $"Terrasse {i}"
                });

                context.Tables.Add(new Table()
                {
                    Places = new Random().Next(2, 8),
                    RoomId = 2,
                    Name = $"Table {i}"

                });
            }

            //for (int i = 0; i < 20; i++)
            //{
            //    context.Bookings.Add(new Booking()
            //    {
            //        Status = (int) BookingStatus.Open,
            //        TableId = new Random().Next(1, 10),
            //        ProductId = new Random().Next(1, 5),
            //        Timestamp = DateTime.Now
            //    });
            //}

            base.Seed(context);
        }
    }

}
