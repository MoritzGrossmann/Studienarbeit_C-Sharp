using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Database;

namespace Buchungssystem.Konsole
{
    class Program
    {
        static void Main(string[] args)
        {
            StammdatenPersistenz stammdatenPersistenz = new StammdatenPersistenz();
            stammdatenPersistenz.Raeume().ForEach(r => Console.WriteLine($"{r.Name} mit Id {r.Id}"));
            Console.ReadKey();
        }
    }
}
