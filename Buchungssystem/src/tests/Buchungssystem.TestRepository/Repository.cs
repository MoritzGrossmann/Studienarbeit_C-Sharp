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
        public static List<Room> Rooms { get; } = new List<Room>()
        {
            new Room()
            {
                Name = "Saal",
                Id = 1
            },
            new Room()
            {
                Name = "Terrasse",
                Id =2
            }
        };

        public static List<Table> Tables { get; } = new List<Table>()
        {
            new Table()
            {
                Id = 1,
                Name = "Tisch 1",
                Places = 4,
                Room = Rooms.FirstOrDefault()
            }, 
            new Table()
            {
                Id = 2,
                Name = "Tisch 2",
                Places = 8,
                Room = Rooms.FirstOrDefault()
            },
            new Table()
            {
                Id = 3,
                Name = "Tisch 3",
                Places = 6,
                Room = Rooms.FirstOrDefault()
            }
        };

        public static List<Product> Products { get; } = new List<Product>()
        {
            new Product()
            {
                Name = "Gin Tonic",
                Price = (decimal)5.5,
                Id = 1
            }, new Product()
                {
                Name = "Whiskey Cola",
                Price = (decimal)6,
                Id = 1
            }, new Product()
            {
                Name = "Wodka Lemon",
                Price = (decimal)5.5,
                Id = 3
            },
            new Product()
            {
                Name = "Wodka Orange",
                Price = (decimal)5.5,
                Id = 4
            },
                new Product()
            {
                Name = "Campari Orange",
                Price = (decimal)5.5,
                Id = 5
            }, new Product()
            {
                Name = "Gin Lemon",
                Price = (decimal)5.5,
                Id = 6
            }
        };

        public static List<ProductGroup> ProductGroups { get; } = new List<ProductGroup>()
        {
            new ProductGroup()
            {
                Id = 1,
                Name = "Longdrinks",
            }
        };

        public List<Booking> Bookings { get; } = new List<Booking>()
        {
            new Booking()
            {
                Id = 1,
                Product = Products.FirstOrDefault(),
                Table = Tables.FirstOrDefault()
            },
            new Booking()
            {
                Id = 2,
                Product = Products.FirstOrDefault(),
                Table = Tables.FirstOrDefault()
            },
            new Booking()
            {
                Id = 2,
                Product = Products.FirstOrDefault(),
                Table = Tables.FirstOrDefault()
            }
        };
    }
}
