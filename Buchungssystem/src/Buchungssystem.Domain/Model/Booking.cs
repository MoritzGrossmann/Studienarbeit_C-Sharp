using System;

namespace Buchungssystem.Domain.Model
{
    public class Booking : BookingSystemModel
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime Finished { get; set; }

        public Product Product { get; set; }

        public Table Table { get; set; }

        public decimal Price { get;  set; }

        public BookingStatus Status { get; set; }
        public Booking Persist()
        {
            Status = BookingStatus.Open;
            Price = Product.Price;
            Created = DateTime.Now;
            Finished = DateTime.Now;
            return Persistence?.Book(this);
        }

        public void Pay()
        {
            Finished = DateTime.Now;
            Persistence?.Pay(this);
            Status = BookingStatus.Paid;
        }

        public void Cancel()
        {
            Finished = DateTime.Now;
            Persistence?.Cancel(this);
            Status = BookingStatus.Paid;

            new Booking() { Finished = DateTime.Now, Product = Product, Table = Table, Status = BookingStatus.Cancled, Persistence = Persistence}.Persist();
        }
    }
}