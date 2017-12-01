using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Repository.Model
{
    public class DbProductGroup
    {
        public DbProductGroup()
        {
            
        }

        [Key]
        public int DbProductGroupId { get; set; }

        public string Name { get; set; }

        public ICollection<DbProduct> DbProducts { get; set; }
    }
}
