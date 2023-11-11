using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DBMySQL.Models
{
    [Index("Name")]
    public class Item : BaseName
    {
        public DateTime PropertiesLastUpdateDate { get; set; } = DateTime.Now;
        public DateTime RecipesLastUpdateDate { get; set; } = DateTime.Now;
        public int SubcategoryId { get; set; }
        [ForeignKey("SubcategoryId")]
        public Subcategory? Subcategory { get; set; }
        public int? ResearchId { get; set; }
        [ForeignKey("ResearchId")]
        public Research? Research { get; set; }
        public List<ItemProperty> ItemProperties { get; set; } = new();
        public List<ItemRecipe> ItemRecipes { get; set; } = new();
        public string ImageName { get; set; } = String.Empty;
    }
}
