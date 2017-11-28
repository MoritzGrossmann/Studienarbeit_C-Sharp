using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Domain.Database
{
    public interface IPersistBaseData
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
    }
}
