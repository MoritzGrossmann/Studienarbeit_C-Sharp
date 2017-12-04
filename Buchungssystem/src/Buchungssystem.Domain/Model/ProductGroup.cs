using System;
using System.Collections.Generic;

namespace Buchungssystem.Domain.Model
{
    public class ProductGroup : BookingSystemModel, IComparable<ProductGroup>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        public override void Persist()
        {
            Persistence.PersistProductGroup(this);
        }

        public int CompareTo(ProductGroup other)
        {
            return String.Compare(Name, other.Name, StringComparison.CurrentCulture);
        }
    }
}