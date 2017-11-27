using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.TestRepository
{
    public class TestPersitence : IPersistBaseData, IPersistBooking
    {
        private static readonly Repository Repository = new Repository();
        public List<Room> Rooms()
        {
            return Repository.Rooms;
        }

        public Room PersistRoom(Room room)
        {
            room.RoomId = Repository.Rooms.Count + 1;
            Repository.Rooms.Add(room);
            return room;
        }

        public List<Table> Tables()
        {
            return Repository.Tables;
        }

        public List<Table> Tables(Room room)
        {
            return Repository.Tables.Where(t => t.RoomId == room.RoomId).ToList();
        }

        public Table PersistTable(Table table)
        {
            table.TableId = Repository.Tables.Count + 1;
            Repository.Tables.Add(table);
            return table;
        }

        public void DeleteTable(Table table)
        {
            throw new NotImplementedException();
        }

        public List<ProductGroup> ProductGroups()
        {
            return Repository.ProductGroups;
        }

        public ProductGroup PersistProductGroup(ProductGroup productGroup)
        {
            productGroup.ProductGroupId = Repository.ProductGroups.Count + 1;
            Repository.ProductGroups.Add(productGroup);
            return productGroup;
        }

        public List<Product> Products(ProductGroup productGroup)
        {
            return Repository.Products.Where(p => p.ProductGroupId == productGroup.ProductGroupId).ToList();
        }

        public Product PersistProduct(Product product)
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
            return Repository.Bookings.Where(b => b.TableId == table.TableId).ToList();
        }

        public List<Booking> Bookings(DateTime dateTime)
        {
            return Repository.Bookings.Where(b => b.Timestamp.Date == dateTime.Date).ToList();
        }

        public List<Booking> Bookings(Table table, BookingStatus status)
        {
            return Repository.Bookings.Where(b => b.TableId == table.TableId && b.Status == (int)status).ToList();
        }

        public Product Product(Booking booking)
        {
            return Repository.Products.FirstOrDefault(p => p.ProductId == booking.ProductId);
        }

        public Table Table(Booking booking)
        {
            return Repository.Tables.FirstOrDefault(t => t.TableId == booking.ProductId);
        }
    }
}
