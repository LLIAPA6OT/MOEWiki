namespace MoeWiki.WebApp.Models.GuildTech
{
    public class GTFilter
    {
        public int TargetId { get; set; }
        public int GuildLvlHave { get; set; }
        public List<int> CheckedIds { get { 
                var result = new List<int>();
                if (IdsStr != null)
                    result = this.IdsStr.Split(',').Where(w => !string.IsNullOrWhiteSpace(w)).Select(s  => int.Parse(s)).ToList();
                return result;
            } 
        }
        public string IdsStr { get; set; }
    }

}
