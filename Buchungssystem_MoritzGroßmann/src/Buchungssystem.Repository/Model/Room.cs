﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Repository.Model
{
    /// <summary>
    /// Datenbankrepräsentation eines Raumes
    /// </summary>
    public class DbRoom
    {
        [Key]
        public int DbRoomId { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        public ICollection<DbTable> DbTables { get; set; }
    }
}
