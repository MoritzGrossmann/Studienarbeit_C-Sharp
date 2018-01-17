using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Repository.Model
{
    /// <summary>
    /// Datenbankrepräsentation einer Warengruppe
    /// </summary>
    public class DbProductGroup
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<DbProduct> Products { get; set; }

        public ICollection<DbProductGroup> ProductGroups { get; set; }

        public int ParentId { get; set; }

        public DbProductGroup ProductGroup { get; set; }

        public bool Deleted { get; set; }
    }
}
