using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stundenplan.Domain.Universal;

namespace Stundenplan.Domain.Database
{
    public interface ISaveTimeTable
    {
        long CreateTimetable(string name);

        long CreateDescipline(string name, string kuerzel, Farbe farbe);


    }
}
