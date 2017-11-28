using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Domain.Database
{
    public interface IPersistBooking
    {
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
