using MOEWiki.DBMySQL.Models;

namespace MoeWiki.WebApp.Models.Home
{
    public class SearchResultModel
    {
        public List<MOEWiki.DBMySQL.Models.Item> items { get; set; }
        public List<Perk> perks { get; set; }
        public List<MOEWiki.DBMySQL.Models.GuildSkill> guildSkills { get; set; }
        public List<Subcategory> subcategories { get; set; }
    }
}
