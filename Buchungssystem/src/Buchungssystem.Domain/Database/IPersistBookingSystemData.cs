using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Domain.Database
{
    public interface IPersistBookingSystemData
    {
        List<Room> Rooms();

        Room PersistRoom(Room room);

        Room Room(Table table);

        List<Table> Tables();

        List<Table> Tables(Room room);

        Table PersistTable(Table table);

        void DeleteTable(Table table);

        List<ProductGroup> ProductGroups();

        ProductGroup PersistProductGroup(ProductGroup productGroup);

        List<Product> Products(ProductGroup productGroup);

        Product PersistProduct(Product product);

        void ChangePrice(Product product, decimal price);

        void DeleteProduct(Product product);

        Booking Book(Booking booking);

        void Cancel(Booking booking);

        void Pay(Booking booking);

        List<Booking> Bookings(Table table);

        List<Booking> Bookings(DateTime dateTime);

        List<Booking> Bookings(Table table, BookingStatus status);

        Product Product(Booking booking);

        Table Table(Booking booking);

        void Occupy(Table table);

        void Clear(Table table);
    }
}
