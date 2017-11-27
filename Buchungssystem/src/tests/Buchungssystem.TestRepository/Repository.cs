using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.TestRepository
{
    internal class Repository
    {
        public List<Room> Rooms { get; } = new List<Room>()
        {
            new Room()
            {
                Name = "Saal",
                RoomId = 1
            }
        };

        public List<Table> Tables { get; } = new List<Table>()
        {
            new Table()
            {
                TableId = 1,
                Name = "Tisch 1",
                Places = 4,
                RoomId = 1
            }, 
            new Table()
            {
                TableId = 2,
                Name = "Tisch 2",
                Places = 8,
                RoomId = 1
            },
            new Table()
            {
                TableId = 3,
                Name = "Tisch 3",
                Places = 6,
                RoomId = 1
            }
        };

        public  List<Product> Products { get; } = new List<Product>()
        {
            new Product()
            {
                Name = "Gin Tonic",
                Price = (decimal)5.5,
                ProductGroupId = 1,
                ProductId = 1
            }, new Product()
                {
                Name = "Whiskey Cola",
                Price = (decimal)6,
                ProductGroupId = 2
                    ,
                    ProductId = 1
            }, new Product()
            {
                Name = "Wodka Lemon",
                Price = (decimal)5.5,
                ProductGroupId = 1
                ,
                ProductId = 3
            },
            new Product()
            {
                Name = "Wodka Orange",
                Price = (decimal)5.5,
                ProductGroupId = 1
                ,
                ProductId = 4
            },
                new Product()
            {
                Name = "Campari Orange",
                Price = (decimal)5.5,
                ProductGroupId = 1,
                ProductId = 1
            }, new Product()
            {
                Name = "Gin Lemon",
                Price = (decimal)5.5,
                ProductGroupId = 1,
                ProductId = 5
            }
        };

        public List<ProductGroup> ProductGroups { get; } = new List<ProductGroup>()
        {
            new ProductGroup()
            {
                ProductGroupId = 1,
                Name = "Longdrinks"
            }
        };

        public List<Booking> Bookings { get; } = new List<Booking>()
        {
            new Booking()
            {
                BookingId = 1,
                ProductId = 1,
                TableId = 1,
                Timestamp = DateTime.Now
            },
            new Booking()
            {
                BookingId = 2,
                ProductId = 5,
                TableId = 1,
                Timestamp = DateTime.Now
            },
            new Booking()
            {
                BookingId = 2,
                ProductId = 3,
                TableId = 1,
                Timestamp = DateTime.Now
            }
        };
    }
}
