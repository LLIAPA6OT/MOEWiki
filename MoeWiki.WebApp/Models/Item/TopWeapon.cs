using MOEWiki.DBMySQL.Models;

namespace MoeWiki.WebApp.Models.Item
{
    public class TopWeapon
    {
        //Filter
        public int[] SubcatsIdsSelected { get; set; }
        public string[] QualitiesSelected { get; set; }
        public string[] LevelsSelected { get; set; }
        public string ActiveTab { get; set; }

        //Order
        public string OrderBy { get; set; } = string.Empty;
        public bool Desc { get; set; } = true;

        //List
        public List<MOEWiki.DBMySQL.Models.Item> Items { get; set; }
    }

    public class SubCat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActiveTab { get; set; }
    }
}
