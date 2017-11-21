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

        List<Table> Tables(Room room);

        List<Table> Tables();

        List<ProductGroup> ProductGroups();
    }
}
