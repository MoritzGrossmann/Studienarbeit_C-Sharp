using System;
using System.Collections.Generic;
using System.Linq;
using Buchungssystem.Domain.Database;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Repository
{
    public class BookingPersistence : IPersistBooking
    {
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
                    (int) BookingStatus.Cancled;
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
                return context.Bookings.Where(b => b.TableId == table.TableId).ToList();
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
                return context.Bookings.Where(b => b.TableId == table.TableId && b.Status == (int) status).ToList();
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
