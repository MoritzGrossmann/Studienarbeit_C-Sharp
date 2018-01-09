using System;
using Buchungssystem.Repository.Database;

namespace Buchungssystem.Konsole
{
    class Program
    {
        static void Main()
        {
            using (new BookingsystemEntities())
            {

            }
            Console.ReadKey();
        }
    }
}
