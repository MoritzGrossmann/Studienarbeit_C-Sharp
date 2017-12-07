using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.TestRepository
{
    public class TestPersitence : IPersistBookingSystemData
    {
        private static readonly Repository Repository = new Repository();
        public List<Room> Rooms()
        {
            return Repository.Rooms;
        }

        public Room PersistRoom(Room room)
        {
            room.Id = Repository.Rooms.Count + 1;
            Repository.Rooms.Add(room);
            return room;
        }

        public Room Room(Table table)
        {
            return Repository.Rooms.FirstOrDefault(r => r.Id == table.Room.Id);
        }

        public List<Table> Tables()
        {
            return Repository.Tables;
        }

        public List<Table> Tables(Room room)
        {
            return Repository.Tables.Where(t => t.Room.Id== room.Id).ToList();
        }

        public Table PersistTable(Table table)
        {
            table.Id = Repository.Tables.Count + 1;
            Repository.Tables.Add(table);
            return table;
        }

        public List<ProductGroup> ProductGroups()
        {
            return Repository.ProductGroups;
        }

        public ProductGroup PersistProductGroup(ProductGroup productGroup)
        {
            productGroup.Id = Repository.ProductGroups.Count + 1;
            Repository.ProductGroups.Add(productGroup);
            return productGroup;
        }

        public List<Product> Products(ProductGroup productGroup)
        {
            return Repository.Products.Where(p =>  ((ProductGroup)p.Parent()).Id == productGroup.Id).ToList();
        }

        public Product PersistProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public List<ProductGroup> ProductGroups(ProductGroup productGroup)
        {
            throw new NotImplementedException();
        }

        public void ChangePrice(Product product, decimal price)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Booking Book(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void Cancel(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void Pay(Booking booking)
        {
            throw new NotImplementedException();
        }

        public List<Booking> Bookings(Table table)
        {
            return Repository.Bookings.Where(b => b.Table.Id == table.Id).ToList();
        }

        public Product Product(Booking booking)
        {
            return Repository.Products.FirstOrDefault(p => p.Id == booking.Product.Id);
        }

        public Table Table(Booking booking)
        {
            return Repository.Tables.FirstOrDefault(t => t.Id == booking.Table.Id);
        }

        public void Occupy(Table table)
        {
            throw new NotImplementedException();
        }

        public void Clear(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
