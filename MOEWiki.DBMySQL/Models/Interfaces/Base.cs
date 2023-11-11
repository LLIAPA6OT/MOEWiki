using System.ComponentModel.DataAnnotations;

namespace MOEWiki.DBMySQL.Models.Interfaces
{
    public class Base : IBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; } = false;
    }
}
