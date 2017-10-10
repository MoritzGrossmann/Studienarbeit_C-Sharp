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
                stundenplan sp = new stundenplan() {Name = name};
                context.stundenplans.Add(sp);

                context.SaveChanges();
                return sp.Id;
            }
        }

        public long CreateDescipline(string name, string kuerzel, Farbe farbe)
        {
            using (var context = new StundenplanEntities())
            {
                stundenplan_fach dis = new stundenplan_fach(){Farbe = (long) farbe, Name = name, Kuerzel = kuerzel};
                context.stundenplan_fach.Add(dis);

                context.SaveChanges();
                return dis.Id;
            }
        }


        public IEnumerable<TimeTable> GetTimeTables()
        {
            using (var context = new StundenplanEntities())
            {
                return context.stundenplans.Select(FromDb);
            }
        }

        private TimeTable FromDb(stundenplan timeTable)
        {
            return new TimeTable()
            {
                Name = timeTable.Name,
                Disciplines = timeTable.stunden.Select(FromDb)
            };
        }

        private Discipline FromDb(studenplan_stunden discipline)
        {
            return new Discipline()
            {
                Name = discipline.fach.Name,
                ShortCut = discipline.fach.Kuerzel
            };
        }
    }
}
