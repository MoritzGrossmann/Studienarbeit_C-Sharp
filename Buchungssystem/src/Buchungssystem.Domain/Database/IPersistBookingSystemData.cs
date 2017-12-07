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

        List<Table> Tables();

        List<Table> Tables(Room room);

        Table PersistTable(Table table);

        List<ProductGroup> ProductGroups();

        ProductGroup PersistProductGroup(ProductGroup productGroup);

        Product PersistProduct(Product product);

        Booking Book(Booking booking);

        void Cancel(Booking booking);

        void Pay(Booking booking);

        List<Booking> Bookings(Table table);

        void Occupy(Table table);

        void Clear(Table table);
    }
}
