using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Domain.Model
{
    public class ProductGroup
    {
        [Key]
        public int ProductGroupId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
