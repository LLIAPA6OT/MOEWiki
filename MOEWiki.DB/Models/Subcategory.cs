using Microsoft.EntityFrameworkCore;
using MOEWiki.DB.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DB.Models
{
    [Index("Name", IsUnique = true)]
    [Index("Synonyms", IsUnique = false)]
    public class Subcategory : BaseName
    {
        public string Synonyms { get; set; } = String.Empty;
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public List<Item> Items { get; set; } = new();
    }
}
