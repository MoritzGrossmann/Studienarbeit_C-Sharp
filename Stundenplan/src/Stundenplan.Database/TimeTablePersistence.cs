using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stundenplan.Domain;
using Stundenplan.Domain.Database;

namespace Stundenplan.Database
{
    public class TimeTablePersistence : ISaveTimeTable, ILoadTimeTable
    {
        public void CreateTimetable(string name)
        {
            using (var context = new StundenplanEntities())
            {
                context.stundenplans.Add(new stundenplan()
                {
                    Name = name
                });

                context.SaveChanges();
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
