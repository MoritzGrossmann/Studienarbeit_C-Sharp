using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buchungssystem.Repository;
using Buchungssystem.Domain.Model;

namespace Buchungssystem.Konsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new BuchungssystemEntities())
            {

                var raeume = new StammdatenPersistenz().Raeume();
                foreach (var raum in raeume)
                {
                    Console.WriteLine($"{raum.RaumId} {raum.Name}");
 
                        new StammdatenPersistenz().Tische(raum).ForEach(t => Console.WriteLine($"\t{t.TischId} {t.Name}"));
                }
            }
            Console.ReadKey();
        }
    }
}
