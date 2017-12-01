using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Booking = Buchungssystem.Repository.Model.Booking;
using Product = Buchungssystem.Repository.Model.Product;
using Room = Buchungssystem.Repository.Model.Room;
using Table = Buchungssystem.Repository.Model.Table;

namespace Buchungssystem.Repository
{
    public class BookingSystemDataPersitence : IPersistBookingSystemData
    {

        #region ProductGroup

        public ProductGroup PersistProductGroup(ProductGroup productGroup)
        { 
            using (var context = new BookingsystemEntities())
            {
                context.ProductGroups.Add(productGroup);
                context.SaveChanges();
                return productGroup;
            }
        }

        public List<ProductGroup> ProductGroups()
        {
            using (var context = new BookingsystemEntities())
            {
                var productGroups = context.ProductGroups.ToList();
                productGroups.ForEach(p => p.Products = Products(p));
                return productGroups;
            }
        }

        #endregion

        #region Product

        public Product PersistProduct(Product product)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Products.Add(product);
                context.SaveChanges();
                return product;
            }
        }

        public List<Product> Products()
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Products.Where(w => !w.Deleted).ToList();
            }
        }

        public List<Product> Products(ProductGroup productGroup)
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Products.Where(w => w.ProductGroupId == productGroup.ProductGroupId && !w.Deleted).ToList();
            }
        }

        public void ChangePrice(Product product, decimal price)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Products.FirstOrDefault(w => w.ProductId == product.ProductId).Price = price;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(Product product)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Products.FirstOrDefault(w => w.ProductId == product.ProductId).Deleted = true;
                context.SaveChanges();
            }
        }

        #endregion

        #region Room

        public List<Room> Rooms()
        {
            using (var context = new BookingsystemEntities())
            {
                var rooms = context.Rooms.ToList();
                rooms.ForEach(r => r.Tables = Tables(r));
                return rooms;
            }
        }

        public Room PersistRoom(Room room)
        {
            using (var context = new BookingsystemEntities())
            { 
                context.Rooms.Add(room);
                context.SaveChanges();
                return room;
            }
        }

        public Room Room(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Rooms.FirstOrDefault(r => r.RoomId == table.RoomId);
            }
        }

        public void DeleteRoom(Room room)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Rooms.Remove(room);
                context.SaveChanges();
            }
        }

        #endregion

        #region Table

        public Table PersistTable(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                table.Occupied = false;
                context.Tables.Add(table);
                context.SaveChanges();
                return table;
            }
        }

        public void DeleteTable(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Tables.Remove(table);
                context.SaveChanges();
            }
        }

        public List<Table> Tables()
        {
            using (var context = new BookingsystemEntities())
            {
                var tables = context.Tables.ToList();
                tables.ForEach(t => t.Bookings = _bookingPersistence.Bookings(t));
                return tables;
            }
        }

        public List<Table> Tables(Room room)
        {
            using (var context = new BookingsystemEntities())
            {
                return Tables().Where(table => table.RoomId == room.RoomId).ToList();
            }
        }

        #endregion

        public Booking Book(Booking booking)
        {
            if (booking.Product != null)
            {
                booking.ProductId = booking.Product.ProductId;
                booking.Product = null;
            }

            if (booking.Table != null)
            {
                booking.TableId = booking.Table.TableId;
                booking.Table = null;
            }

            using (var context = new BookingsystemEntities())
            {
                context.Bookings.Add(booking);
                context.SaveChanges();
                return booking;
            }
        }

        public void Cancel(Booking booking)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Bookings.FirstOrDefault(b => b.BookingId == booking.BookingId).Status =
                    (int)BookingStatus.Cancled;
                context.SaveChanges();
            }
        }

        public void Pay(Booking booking)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Bookings.FirstOrDefault(b => b.BookingId == booking.BookingId).Status =
                    (int)BookingStatus.Paid;
                context.SaveChanges();
            }
        }

        public List<Booking> Bookings(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                var bookings = context.Bookings.Where(b => b.TableId == table.TableId).ToList();
                bookings.ForEach(b => b.Product = Product(b));
                return bookings;
            }
        }

        public List<Booking> Bookings(DateTime dateTime)
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Bookings.Where(b => b.Timestamp.Date == dateTime.Date).ToList();
            }
        }

        public List<Booking> Bookings(Table table, BookingStatus status)
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Bookings.Where(b => b.TableId == table.TableId && b.Status == (int)status).ToList();
            }
        }

        public Product Product(Booking booking)
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Products.FirstOrDefault(p => p.ProductId == booking.ProductId);
            }
        }

        public Table Table(Booking booking)
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Tables.FirstOrDefault(t => t.TableId == booking.TableId);
            }
        }

        public void Occupy(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Tables.FirstOrDefault(t => t.TableId == table.TableId).Occupied = true;
                context.SaveChanges();
            }
        }

        public void Clear(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Tables.FirstOrDefault(t => t.TableId == table.TableId).Occupied = false;
                context.SaveChanges();
            }
        }
    }
}
