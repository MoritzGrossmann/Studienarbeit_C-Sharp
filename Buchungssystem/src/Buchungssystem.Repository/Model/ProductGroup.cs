using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buchungssystem.Repository.Model
{
    public class DbProductGroup
    {
        public DbProductGroup()
        {
            
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<DbProduct> Products { get; set; }

        public ICollection<DbProductGroup> ProductGroups { get; set; }

        public int ParentId { get; set; }

        public DbProductGroup ProductGroup { get; set; }
    }
}
