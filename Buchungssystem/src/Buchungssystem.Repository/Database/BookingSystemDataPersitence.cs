using System;
using System.Collections.Generic;
using System.Linq;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository.Model;
using static Buchungssystem.Domain.Model.BookingStatus;

namespace Buchungssystem.Repository.Database
{
    public class BookingSystemDataPersitence : IPersistBookingSystemData
    {

        #region ProductGroup

        public ProductGroup PersistProductGroup(ProductGroup productGroup)
        { 
            using (var context = new BookingsystemEntities())
            {
                var dbProductsGroup = FromProductGroup(productGroup);
                context.ProductGroups.Add(dbProductsGroup);
                context.SaveChanges();
                productGroup.Id = dbProductsGroup.DbProductGroupId;
                return productGroup;
            }
        }

        public List<ProductGroup> ProductGroups()
        {
            using (var context = new BookingsystemEntities())
            {
                return context.ProductGroups.Select(FromDbProductGroup).ToList();
            }
        }

        #endregion

        #region Product

        public Product PersistProduct(Product product)
        {
            using (var context = new BookingsystemEntities())
            {
                var dbProduct = FromProduct(product);
                context.Products.Add(dbProduct);
                context.SaveChanges();
                product.Id = dbProduct.DbProductId;
                return product;
            }
        }

        public List<Product> Products()
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Products.Where(w => !w.Deleted).AsEnumerable()?.Select(FromDbProduct).ToList();
            }
        }

        public List<Product> Products(ProductGroup productGroup)
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Products.Where(w => w.DbProductGroupId == productGroup.Id && !w.Deleted).AsEnumerable()?.Select(FromDbProduct).ToList();
            }
        }

        public Product Product(int id)
        {
            using (var context = new BookingsystemEntities())
            {
                return FromDbProduct(context.Products.FirstOrDefault(p => p.DbProductId == id));
            }
        }

        public void ChangePrice(Product product)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Products.FirstOrDefault(p => p.DbProductId == product.Id).Price = product.Price;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(Product product)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Products.FirstOrDefault(w => w.DbProductId == product.Id).Deleted = true;
                context.SaveChanges();
            }
        }

        #endregion

        #region Room

        public List<Room> Rooms()
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Rooms.Select(FromDbRoom).ToList();
            }
        }

        public Room PersistRoom(Room room)
        {
            using (var context = new BookingsystemEntities())
            {
                var dbRoom = FromRoom(room);
                context.Rooms.Add(dbRoom);
                context.SaveChanges();
                room.Id = dbRoom.DbRoomId;
                return room;
            }
        }

        #endregion

        #region Table

        public Table PersistTable(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                var dbTable = FromTable(table);
                table.Occupied = false;
                context.Tables.Add(dbTable);
                context.SaveChanges();
                return table;
            }
        }

        public List<Table> Tables()
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Tables.Select(FromDbTable).ToList();
            }
        }

        public List<Table> Tables(Room room)
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Tables.Where(t => t.DbRoomId == room.Id).AsEnumerable().Select(FromDbTable).ToList();
            }
        }

        #endregion

        public Booking Book(Booking booking)
        { 
            using (var context = new BookingsystemEntities())
            {
                var dbBooking = FromBooking(booking);
                context.Bookings.Add(dbBooking);
                context.SaveChanges();
                booking.Id = dbBooking.DbBookingId;
                return booking;
            }
        }

        public void Cancel(Booking booking)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Bookings.FirstOrDefault(b => b.DbBookingId == booking.Id).Status =
                    (int)Cancled;
                context.SaveChanges();
            }
        }

        public void Pay(Booking booking)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Bookings.FirstOrDefault(b => b.DbBookingId == booking.Id).Status =
                    (int)Paid;
                context.SaveChanges();
            }
        }

        public List<Booking> Bookings(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                var bookings = context.Bookings.Where(b => b.DbTableId == table.Id).AsEnumerable().Select(FromDbBooking).ToList();
                bookings.ForEach(b => b.Table = table);
                return bookings;
            }
        }

        public void Occupy(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Tables.FirstOrDefault(t => t.DbTableId == table.Id).Occupied = true;
                context.SaveChanges();
            }
        }

        public void Clear(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Tables.FirstOrDefault(t => t.DbTableId == table.Id).Occupied = false;
                context.SaveChanges();
            }
        }

        private DbRoom FromRoom(Room room)
        {
            return new DbRoom(){ DbRoomId = room.Id, Name = room.Name};
        }

        private Room FromDbRoom(DbRoom dbRoom)
        {
            var room = new Room() {Id = dbRoom.DbRoomId, Name = dbRoom.Name};
            room.Tables = Tables(room);
            room.Tables.ToList().ForEach(t => t.Room = room);
            return room;
        }


        private DbProduct FromProduct(Product product)
        {
            return new DbProduct() {DbProductGroupId = product.ProductGroup.Id, Deleted = false, Name = product.Name, Price = product.Price};
        }

        private Product FromDbProduct(DbProduct dbProduct)
        {
            return new Product() {Id = dbProduct.DbProductId, Name = dbProduct.Name, Price = dbProduct.Price};
        }

        private DbProductGroup FromProductGroup(ProductGroup productGroup)
        {
            return new DbProductGroup() {Name = productGroup.Name, DbProductGroupId = productGroup.Id};
        }

        private ProductGroup FromDbProductGroup(DbProductGroup dbProductGroup)
        {
            var productsGroup = new ProductGroup() {Id = dbProductGroup.DbProductGroupId, Name = dbProductGroup.Name };
            productsGroup.Products = Products(productsGroup);
            productsGroup.Products.ToList().ForEach(p => p.ProductGroup = productsGroup);
            return productsGroup;
        }

        private DbTable FromTable(Table table)
        {
            return new DbTable() {Name = table.Name, Occupied = table.Occupied, DbRoomId = table.Room.Id};
        }

        private Table FromDbTable(DbTable dbTable)
        {
            var table = new Table() {Name = dbTable.Name, Occupied = dbTable.Occupied, Id = dbTable.DbTableId, Places = dbTable.Places};
            table.Bookings = Bookings(table);
            table.Bookings.ToList().ForEach(b => b.Table = table);
            return table;
        }

        private DbBooking FromBooking(Booking booking)
        {
            return new DbBooking(){DbBookingId = booking.Id, DbTableId = booking.Table.Id, DbProductId = booking.Product.Id, Status = (int)booking.Status};
        }

        private Booking FromDbBooking(DbBooking dbBooking)
        {
            var booking = new Booking() { Id = dbBooking.DbBookingId, Status = (BookingStatus)dbBooking.Status};
            booking.Product = Product(dbBooking.DbProductId);
            return booking;
        }
    }
}
