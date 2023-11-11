using Microsoft.EntityFrameworkCore;
using MOEWiki.DB.Models.Interfaces;

namespace MOEWiki.DB.Models
{
    [Index("Name", IsUnique = true)]
    [Index("Synonyms", IsUnique = false)]
    public class Category : BaseName
    {
        public string Synonyms { get; set; } = String.Empty;

        public List<Subcategory> Subcategories { get; set; } = new();
        public List<Item> Items { get; set; } = new();
    }
}
