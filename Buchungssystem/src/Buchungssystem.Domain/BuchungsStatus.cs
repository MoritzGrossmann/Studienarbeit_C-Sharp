using System.ComponentModel.DataAnnotations;

namespace Buchungssystem.Domain
{
    public class BuchungsStatus
    {
        [Key]
        public int BuchungsStatusId { get; set; }
        public string Bezeichnung { get; set; }
    }
}