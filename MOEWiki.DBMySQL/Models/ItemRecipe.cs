using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOEWiki.DBMySQL.Models
{
    public class ItemRecipe : Base
    {
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
        public int RecipeItemId { get; set; }
        public string RecipeItemName { get; set; } = string.Empty;
        public int Count { get; set; }
        public int ForQuantity { get; set; } = 1;
        public int Number {  get; set; }
        public bool IsStepByStep { get; set; } = false;

    }
}
