namespace MOEWiki.DBMySQL.Models.Interfaces
{
    public class BaseName : Base,IName
    {
        public string Name {get; set;} = String.Empty;
    }
}
