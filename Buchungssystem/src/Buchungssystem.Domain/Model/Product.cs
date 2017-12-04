using System;

namespace Buchungssystem.Domain.Model
{
    public class Product : BookingSystemModel, IComparable<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductGroup ProductGroup { get; set; }
        public override void Persist()
        {
            Persistence.PersistProduct(this);
        }

        public int CompareTo(Product other)
        {
            return String.Compare(Name, other.Name, StringComparison.CurrentCulture);
        }
    }
}