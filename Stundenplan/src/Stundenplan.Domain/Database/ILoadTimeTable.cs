﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan.Domain.Database
{
    public interface ILoadTimeTable
    {
        IEnumerable<TimeTable> GetTimeTables();
    }
}