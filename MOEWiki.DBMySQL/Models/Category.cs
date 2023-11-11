using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Models.Interfaces;

namespace MOEWiki.DBMySQL.Models
{
    [Index("Name", IsUnique = true)]
    [Index("Synonyms", IsUnique = false)]
    public class Category : BaseName
    {
        public string Synonyms { get; set; } = String.Empty;

        public List<Subcategory> Subcategories { get; set; } = new();
    }
}
