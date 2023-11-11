namespace MOEWiki.DBMySQL.Models.Interfaces
{
    public interface IBase
    {
        int Id { get; set; }
        DateTime CreationDate { get; set; }
        bool IsDelete { get; set; }
    }
    public interface IName
    {
        string Name { get; set; }
    }
}
