using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class WikiActionGateway : BaseGateway
    {
        public WikiActionGateway(string connectionString) : base(connectionString) { }

        public WikiAction GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.wikiactions.AsNoTracking().First(f => f.Id == id);
        }
        public List<WikiAction> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.wikiactions.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public WikiAction Add(WikiAction entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.wikiactions.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public void GenerateThenAdd(string who, string what)
        {
            using (var _context = new ApplicationContext(options))
                Add(new WikiAction() { Who = who, What = what});
        }
    }
}
