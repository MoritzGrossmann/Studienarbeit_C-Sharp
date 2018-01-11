using System.Collections.Generic;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Domain.Database
{
    public interface IPersistBookingSystemData
    {
        List<Room> Rooms();

        Room PersistRoom(Room room);

        void DeleteRoom(Room room);

        void DeleteTable(Table table);

        Table PersistTable(Table table);

        List<ProductGroup> ProductGroups();

        List<ProductGroup> LeafProductGroups();
        
        ProductGroup PersistProductGroup(ProductGroup productGroup);

        void DeleteProductGroup(ProductGroup productGroup);

        List<Product> Products();

        Product PersistProduct(Product product);

        void DeleteProduct(Product product);

        Booking Book(Booking booking);

        void Cancel(Booking booking);

        void Pay(Booking booking);

        void Occupy(Table table);

        void Clear(Table table);
    }
}
