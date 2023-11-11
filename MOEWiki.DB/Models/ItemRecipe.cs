using MOEWiki.DB.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOEWiki.DB.Models
{
    public class ItemRecipe : Base
    {
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
        public int RecipeItemId { get; set; }
        public string RecipeItemName { get; set; } = string.Empty;
        public int Count { get; set; }
        public int Number {  get; set; }
        public bool IsStepByStep { get; set; } = false;
    }
}
