namespace MoeWiki.WebApp.Models.GuildSkill
{
    public class GSFilter
    {
        public int TargetId { get; set; }
        public int TargetValue { get; set; }
        public int GuildLvlHave { get; set; }
        public Dictionary<int, int> ReqPairs { get { 
                var result = new Dictionary<int, int>();
                if (IdsStr == null) return result;
                var ids = this.IdsStr.Split(',').Where(w => !string.IsNullOrWhiteSpace(w)).Select(s  => int.Parse(s)).ToArray();
                var vals = this.ValsStr.Split(',').Where(w => !string.IsNullOrWhiteSpace(w)).Select(s  => int.Parse(s)).ToArray();
                for (int i = 0; i < ids.Length; i++)
                {
                    result.Add(ids[i], vals[i]);
                }
                return result;
            } 
        }
        public string IdsStr { get; set; }
        public string ValsStr { get; set; }
    }

}
