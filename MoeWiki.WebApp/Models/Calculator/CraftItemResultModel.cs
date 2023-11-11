using MoeWiki.WebApp.Models.Item;
using MOEWiki.DBMySQL.Models;

namespace MoeWiki.WebApp.Models.Calculator
{
    public class CraftItemResultModel
    {
        public List<RecipeItem> primitiveItems { get; set; } = new List<RecipeItem>();
        public List<ItemRecipe> costItems { get; set; } = new List<ItemRecipe>();
    }
}
