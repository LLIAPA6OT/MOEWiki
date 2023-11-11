using Microsoft.EntityFrameworkCore;
using MOEWiki.DB.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DB.Models
{
    [Index("Name")]
    public class Item : BaseName
    {
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public int SubcategoryId { get; set; }
        [ForeignKey("SubcategoryId")]
        public Subcategory? Subcategory { get; set; }
        public List<ItemProperty> ItemProperties { get; set; } = new();
        public string ImageName { get; set; } = String.Empty;
    }
}
