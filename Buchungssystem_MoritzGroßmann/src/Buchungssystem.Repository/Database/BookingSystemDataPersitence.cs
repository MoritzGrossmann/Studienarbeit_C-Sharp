﻿using System;
using System.Collections.Generic;
using System.Linq;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;
using Buchungssystem.Repository.Model;
using static Buchungssystem.Domain.Model.BookingStatus;
// ReSharper disable PossibleNullReferenceException

namespace Buchungssystem.Repository.Database
{
    public class BookingSystemDataPersitence : IPersistBookingSystemData
    {

        #region ProductGroup

        public ProductGroup PersistProductGroup(ProductGroup productGroup)
        { 
            using (var context = new BookingsystemEntities())
            {
                if (productGroup.Id > 0)
                    return UpdateProductGroup(productGroup, context);

                if (context.ProductGroups.Any(p=> p.Name == productGroup.Name && p.Deleted == false)) throw new ModelExistException($"Eine Warengruppe mit dem Name {productGroup.Name} exisitert bereits!");

                var dbProductsGroup = FromProductGroup(productGroup);
                context.ProductGroups.Add(dbProductsGroup);
                context.SaveChanges();
                productGroup.Id = dbProductsGroup.Id;
                return productGroup;
            }
        }

        private ProductGroup UpdateProductGroup(ProductGroup productGroup, BookingsystemEntities context)
        {
            var dbProductGroup = context.ProductGroups.FirstOrDefault(p => p.Id == productGroup.Id);
            dbProductGroup.Name = productGroup.Name;
            dbProductGroup.ParentId = ((ProductGroup) productGroup.Parent()).Id;
            context.SaveChanges();

            return productGroup;
        }

        private ProductGroup ProductGroup(DbProduct product)
        {
            using (var context = new BookingsystemEntities())
            {
                return FromDbProductGroup(context.ProductGroups.FirstOrDefault(p => p.Id == product.DbProductGroupId));
            }
        }

        public List<ProductGroup> RootProductGroups()
        {
            using (var context = new BookingsystemEntities())
            {
                var roots =  context.ProductGroups.Where(p => p.ParentId == p.Id && p.Deleted == false).AsEnumerable().Select(FromDbProductGroup).ToList();
                roots.ForEach(p => LoadChilds(p, context));
                return roots;
            }
        }

        public List<ProductGroup> ProductGroups()
        {
            using (var context = new BookingsystemEntities())
            {
                var productGroups = context.ProductGroups.Where(p => p.Deleted == false).AsEnumerable().Select(FromDbProductGroup).ToList();
                productGroups.ForEach(p => LoadChilds(p,context));
                return productGroups;
            }
        }

        private DbProductGroup ParentProductGroup(DbProductGroup productGroup)
        {
            using (var context = new BookingsystemEntities())
            {
                return context.ProductGroups.FirstOrDefault(p => p.Id == productGroup.ParentId);
            }
        }

        public List<ProductGroup> LeafProductGroups()
        {
            using (var context = new BookingsystemEntities())
            {
                return context.ProductGroups.Where(p => !context.ProductGroups.Any(c => c.ParentId == p.Id) && p.Deleted == false).AsEnumerable().Select(FromDbProductGroup).ToList();
            }
        }

        private void LoadChilds(ProductGroup productGroup, BookingsystemEntities context)
        {
            var childnodes = context.ProductGroups.Where(p => p.ParentId == productGroup.Id && p.ParentId != p.Id && p.Deleted == false).AsEnumerable().Select(FromDbProductGroup).ToList();
            context.Products.Where(p => p.DbProductGroupId == productGroup.Id && p.Deleted == false).AsEnumerable()
                .Select(FromDbProduct).ToList().ForEach(productGroup.AddNode);


            if (childnodes.Any(c => c.Id != productGroup.Id))
            {
                childnodes.ForEach(productGroup.AddNode);

                foreach (var c in childnodes)
                {
                    LoadChilds(c, context);
                }
            }
        }

        public void DeleteProductGroup(ProductGroup productGroup)
        {
            using (var context = new BookingsystemEntities())
            {
                context.ProductGroups.FirstOrDefault(p => p.Id == productGroup.Id).Deleted = true;
                context.SaveChanges();
            }
        }

        #endregion

        #region Product

        public Product PersistProduct(Product product)
        {
            using (var context = new BookingsystemEntities())
            {
                if (context.Products.Any(p => p.DbProductId == product.Id)) return UpdateProduct(product, context);

                if (context.Products.Any(p => p.Name.Equals(product.Name) && p.Deleted == false)) throw new ModelExistException($"Ein Product mit dem Name {product.Name} exisitert bereits!");

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

        private Product UpdateProduct(Product product, BookingsystemEntities context)
        {
            var dbProduct = context.Products.FirstOrDefault(p => p.DbProductId == product.Id);
            if (dbProduct == null) throw new ModelNotExistException($"Das PRoduct mit der Id {product.Id} ist nicht vorhanden");
            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.DbProductGroupId = ((ProductGroup) product.Parent()).Id;
            context.SaveChanges();
            return product;
        }

        #endregion

        #region Room

        public List<Room> Rooms()
        {
            using (var context = new BookingsystemEntities())
            {
                return context.Rooms.Where(r => r.Deleted == false).AsEnumerable().Select(FromDbRoom).ToList();
            }
        }

        public Room PersistRoom(Room room)
        {
            using (var context = new BookingsystemEntities())
            {
                if (context.Rooms.Any(r => r.DbRoomId == room.Id)) return UpdateRoom(room, context);

                if (context.Rooms.Any(r => r.Name.Equals(room.Name) && r.Deleted == false)) throw new ModelExistException($"Ein Raum mit dem Name {room.Name} exisitert bereits!");

                var dbRoom = FromRoom(room);
                context.Rooms.Add(dbRoom);
                context.SaveChanges();
                room.Id = dbRoom.DbRoomId;
                return room;
            }
        }

        private Room UpdateRoom(Room room, BookingsystemEntities context)
        {
            var dbroom = context.Rooms.FirstOrDefault(r => r.DbRoomId == room.Id);
            if (dbroom == null) throw new ModelNotExistException($"Der Raum mit der Id {room.Id} ist nicht vorhanden");
            dbroom.Name = room.Name;
            context.SaveChanges();
            return FromDbRoom(dbroom);
        }

        public void DeleteRoom(Room room)
        {
            using (var context = new BookingsystemEntities())
            {
                if (context.Tables.Any(t => t.RoomId == room.Id && context.Bookings.Any(b => b.TableId == t.Id && (BookingStatus)b.Status == Open))) throw new DeleteNotAllowedException($"Löschen nicht möglich: In diesem Raum befinden sich Tische mit offenen Buchungen");
                context.Rooms.FirstOrDefault(r => r.DbRoomId == room.Id).Deleted = true;
                context.SaveChanges();
            }
        }

        #endregion

        #region Table

        public Table PersistTable(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                if (context.Tables.Any(t => t.Id == table.Id)) return UpdateTable(table, context);

                if (context.Tables.Any(t => t.Name.Equals(table.Name) && t.RoomId == table.Room.Id && t.Deleted == false)) throw new ModelExistException($"Ein Tisch mit dem Name {table.Name} exisitert bereits in diesem Raum!");

                var dbTable = FromTable(table);
                table.Occupied = false;
                context.Tables.Add(dbTable);
                context.SaveChanges();
                table.Id = dbTable.Id;
                return table;
            }
        }

        private Table UpdateTable(Table table, BookingsystemEntities context)
        {
            var dbTable = context.Tables.FirstOrDefault(t => t.Id == table.Id);
            if (dbTable == null) throw new ModelNotExistException($"Der Tisch mit der Id {table.Id} ist nicht vorhanden");
            dbTable.Name = table.Name;
            dbTable.Places = table.Places;
            context.SaveChanges();
            return FromDbTable(dbTable);
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
                return context.Tables.Where(t => t.RoomId == room.Id && t.Deleted == false).AsEnumerable().Select(FromDbTable).ToList();
            }
        }

        public void Occupy(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Tables.FirstOrDefault(t => t.Id == table.Id).Occupied = true;
                context.SaveChanges();
            }
        }

        public void Clear(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Tables.FirstOrDefault(t => t.Id == table.Id).Occupied = false;
                context.SaveChanges();
            }
        }

        public void DeleteTable(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                if (context.Bookings.Any(b => b.TableId == table.Id && (BookingStatus)b.Status == Open)) throw new DeleteNotAllowedException($"Löschen nicht möglich: Auf dem Tisch befinden sich noch offene Buchungen");
                context.Tables.FirstOrDefault(t => t.Id == table.Id).Deleted = true;
                context.SaveChanges();
            }
        }

        #endregion

        #region Booking
        public Booking Book(Booking booking)
        { 
            using (var context = new BookingsystemEntities())
            {
                var dbBooking = FromBooking(booking);
                context.Bookings.Add(dbBooking);
                context.SaveChanges();
                booking.Id = dbBooking.Id;
                return booking;
            }
        }

        public void Cancel(Booking booking)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Bookings.FirstOrDefault(b => b.Id == booking.Id).Status =
                    (int)Cancled;
                context.SaveChanges();
            }
        }

        public void Pay(Booking booking)
        {
            using (var context = new BookingsystemEntities())
            {
                context.Bookings.FirstOrDefault(b => b.Id == booking.Id).Status =
                    (int)Paid;
                context.SaveChanges();
            }
        }

        public List<Booking> Bookings(Table table)
        {
            using (var context = new BookingsystemEntities())
            {
                var bookings = context.Bookings.Where(b => b.TableId == table.Id).AsEnumerable().Select(FromDbBooking).ToList();
                bookings.ForEach(b => b.Table = table);
                return bookings;
            }
        }

        public List<Booking> Bookings(DateTime date)
        {
            using (var context = new BookingsystemEntities())
            {
                var bookings = context.Bookings.Where(b => b.Created.Value.Day == date.Day && b.Created.Value.Month == date.Month && b.Created.Value.Year == date.Year).AsEnumerable().Select(FromDbBooking).ToList();
                return bookings;
            }
        }

        #endregion

        #region Converter

        #region Room

        /// <summary>
        /// Konvertiert Room in DbRoom
        /// </summary>
        /// <param name="room">Room, welcher konvertiert werden soll</param>
        /// <returns></returns>
        private DbRoom FromRoom(Room room)
        {
            return new DbRoom() { DbRoomId = room.Id, Name = room.Name };
        }

        /// <summary>
        /// Konvertiert DbRoom in Room
        /// Lädt alle Tische des Raumes, sowie die Buchungen auf den Tischen nach
        /// </summary>
        /// <param name="dbRoom"></param>
        /// <returns></returns>
        private Room FromDbRoom(DbRoom dbRoom)
        {
            var room = new Room() { Id = dbRoom.DbRoomId, Name = dbRoom.Name };
            room.Tables = Tables(room);
            room.Tables.ToList().ForEach(t => t.Room = room);
            room.Persistence = this;
            return room;
        }

        #endregion

        #region Product

        /// <summary>
        /// Konvertiert Product in DbProduct
        /// </summary>
        /// <param name="product">Product, welche konvertiert werden soll</param>
        /// <returns></returns>
        private DbProduct FromProduct(Product product)
        {
            var productGroup = (ProductGroup)product.Parent();
            return new DbProduct() { DbProductGroupId = productGroup.Id, Deleted = false, Name = product.Name, Price = product.Price };
        }

        /// <summary>
        /// Konvertiert DbProduct in Product
        /// Lädt den Parent der Ware nach
        /// </summary>
        /// <param name="dbProduct"></param>
        /// <returns></returns>
        private Product FromDbProduct(DbProduct dbProduct)
        {
            var product = new Product() { Id = dbProduct.DbProductId, Name = dbProduct.Name, Price = dbProduct.Price };
            product.SetParent(ProductGroup(dbProduct));
            product.Persistence = this;
            return product;
        }

        #endregion

        #region ProductGroup

        /// <summary>
        /// Konvertiert ProductGroup in DbProductGroup
        /// </summary>
        /// <param name="productGroup">ProductGroup, welche konvertiert werden soll</param>
        /// <returns></returns>
        private DbProductGroup FromProductGroup(ProductGroup productGroup)
        {
            var parent = (ProductGroup) productGroup.Parent();
            return new DbProductGroup() { Name = productGroup.Name, Id = productGroup.Id, ParentId = parent.Id};
        }

        /// <summary>
        /// Konvertiert DbProductGroup in ProductGroup
        /// Lädt deren Parent nach
        /// </summary>
        /// <param name="dbProductGroup">DbProductGroup, welche konvertiert werden soll</param>
        /// <returns></returns>
        private ProductGroup FromDbProductGroup(DbProductGroup dbProductGroup)
        {
            var productsGroup = new ProductGroup
            {
                Id = dbProductGroup.Id,
                Name = dbProductGroup.Name,
                Persistence = this
            };

            productsGroup.SetParent(dbProductGroup.Id == dbProductGroup.ParentId
                ? productsGroup
                : FromDbProductGroup(ParentProductGroup(dbProductGroup)));

            return productsGroup;
        }

        #endregion

        #region Table

        /// <summary>
        /// Konvertiert Table in DbTable
        /// </summary>
        /// <param name="table">Tisch, welcher konvertiert werden soll</param>
        /// <returns></returns>
        private DbTable FromTable(Table table)
        {
            return new DbTable() { Name = table.Name, Occupied = table.Occupied, RoomId = table.Room.Id, Id = table.Id, Places = table.Places};
        }

        /// <summary>
        /// Konvertiert DbTable in Table
        /// Lädt alle Buchungen von diesem Tisch nach
        /// </summary>
        /// <param name="dbTable">DbTable, welcher konvertiert werden soll</param>
        /// <returns></returns>
        private Table FromDbTable(DbTable dbTable)
        {
            var table = new Table() { Name = dbTable.Name, Occupied = dbTable.Occupied, Id = dbTable.Id, Places = dbTable.Places };
            table.Bookings = Bookings(table);
            table.Bookings.ToList().ForEach(b => b.Table = table);
            table.Persistence = this;
            return table;
        }

        #endregion

        #region Booking

        /// <summary>
        /// Konvertiert Booking in DbBooking
        /// </summary>
        /// <param name="booking">Buchung, welche Konvertiert werden soll</param>
        /// <returns></returns>
        private DbBooking FromBooking(Booking booking)
        {
            return new DbBooking() { Id = booking.Id, TableId = booking.Table.Id, ProductId = booking.Product.Id, Status = (int)booking.Status, Created = booking.Created, Finished = booking.Finished, Price = booking.Price };
        }

        /// <summary>
        /// Kpnvertiert DbBooking in Booking
        /// Wenn Created nicht exisitert, dann ist Created DateTime.MinValue
        /// Wenn Finished nicht exisitert, dann istr Finished DateTime.MinValue
        /// </summary>
        /// <param name="dbBooking">DbBooking, welches konvertiert werden soll</param>
        /// <returns></returns>
        private Booking FromDbBooking(DbBooking dbBooking)
        {
            var booking = new Booking
            {
                Id = dbBooking.Id,
                Status = (BookingStatus) dbBooking.Status,
                Created = dbBooking.Created ?? DateTime.MinValue,
                Finished = dbBooking.Finished ?? DateTime.MinValue,
                Price = dbBooking.Price,
                Product = Product(dbBooking.ProductId),
                Persistence = this
            };
            return booking;
        }

        #endregion

        #endregion

    }
}
