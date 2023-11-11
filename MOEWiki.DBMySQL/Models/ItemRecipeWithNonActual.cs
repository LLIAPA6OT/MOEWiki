using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOEWiki.DBMySQL.Models
{
    public class ItemRecipeWithNonActual : Base
    {
        public static implicit operator ItemRecipeWithNonActual(ItemRecipe itemRecipe)
        {
            var entity = new ItemRecipeWithNonActual();
            itemRecipe.MapTo(entity);
            entity.ItemRecipeId = itemRecipe.Id;
            entity.Id = 0;
            entity.CreationDate = DateTime.Now;
            return entity;
        }
        public int ItemId { get; set; }
        public int ItemRecipeId { get; set; }
        public int RecipeItemId { get; set; }
        public string RecipeItemName { get; set; } = string.Empty;
        public int Count { get; set; }
        public int Number {  get; set; }
    }
}
