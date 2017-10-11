using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stundenplan.Domain;
using Stundenplan.Domain.Database;
using Stundenplan.Domain.Universal;

namespace Stundenplan.Database
{
    public class TimeTablePersistence : ISaveTimeTable, ILoadTimeTable
    {
        public long CreateTimetable(string name)
        {
            using (var context = new StundenplanEntities())
            {
                Stundenplan sp = new Stundenplan() {Name = name};
                context.Stundenplans.Add(sp);

                context.SaveChanges();
                return sp.Id;
            }
        }

        public long CreateDescipline(string name, string kuerzel, Farbe farbe)
        {
            using (var context = new StundenplanEntities())
            {
                Stundenplan_Fach dis = new Stundenplan_Fach(){Farbe = (long) farbe, Name = name, Kuerzel = kuerzel};
                context.Stundenplan_Fach.Add(dis);

                context.SaveChanges();
                return dis.Id;
            }
        }


        public IEnumerable<TimeTable> GetTimeTables()
        {
            using (var context = new StundenplanEntities())
            {
                return context.Stundenplans.Select(FromDb);
            }
        }

        private TimeTable FromDb(Stundenplan timeTable)
        {
            return new TimeTable()
            {
                Name = timeTable.Name,
                Disciplines = timeTable.Stunden.Select(FromDb)
            };
        }

        private Discipline FromDb(Studenplan_Stunden discipline)
        {
            return new Discipline()
            {
                Name = discipline.Fach.Name,
                ShortCut = discipline.Fach.Kuerzel
            };
        }
    }
}
