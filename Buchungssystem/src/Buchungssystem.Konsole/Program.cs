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
            using (var context = new BookingsystemEntities())
            {

                var raeume = new BaseDataPersitence().Rooms();
                foreach (var raum in raeume)
                {
                    Console.WriteLine($"{raum.RoomId} {raum.Name}");
 
                        new BaseDataPersitence().Tables(raum).ForEach(t => Console.WriteLine($"\t{t.TableId} {t.Name}"));
                }
            }
            Console.ReadKey();
        }
    }
}
