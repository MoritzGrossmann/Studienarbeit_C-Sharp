using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Repository
{
    public class BaseDataPersitence : IPersistBaseData
    {
        private IPersistBooking _bookingPersistence = new BookingPersistence();

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
                return context.ProductGroups.ToList();
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
                return context.Rooms.ToList();
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
                return context.Tables.ToList();
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
    }
}
