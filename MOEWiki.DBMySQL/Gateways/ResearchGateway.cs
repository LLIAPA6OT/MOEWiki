using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class ResearchGateway : BaseGateway
    {
        public ResearchGateway(string connectionString) : base(connectionString) { }

        public Research GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.researches.AsNoTracking().First(f => f.Id == id);
        }
        public Research GetByIdIncludeItems(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.researches.AsNoTracking().Include(i => i.Items).First(f => f.Id == id);
        }
        public Research? GetByName(string name)
        {
            using (var _context = new ApplicationContext(options))
                return _context.researches.AsNoTracking().FirstOrDefault(f => f.Name == name);
        }
        public List<Research> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.researches.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<Research> GetAllIncludeItems()
        {
            using (var _context = new ApplicationContext(options))
                return _context.researches.AsNoTracking().Include(i => i.Items).Where(w => !w.IsDelete).ToList();
        }
        public List<Research> GetAllForTechTree()
        {
            using (var _context = new ApplicationContext(options))
                return _context.researches.AsNoTracking().Include(i => i.Items).Where(w => !w.IsDelete && w.ResearchBranch == ResearchBranchEnum.Guild).ToList();
        }
        public List<Research> GetForFilterById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return GetParents(new List<Research>() { GetByIdIncludeItems(id) });
        }
        private List<Research> GetParents(List<Research> list)
        {
            var parentIds = list.Select(s => s.PreviousId).Distinct();
            if (!parentIds.Any()) return list;
            using (var _context = new ApplicationContext(options))
                list.AddRange(GetParents(_context.researches.AsNoTracking().Include(i => i.Items).Where(w => parentIds.Contains(w.Id)).ToList()));
            return list;
        }
        public List<Research> GetAllWithDeletedAndNotActual()
        {
            using (var _context = new ApplicationContext(options))
                return _context.researches.AsNoTracking().ToList();
        }
        public Research Add(Research entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.researches.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public Research Update(Research entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                var v = _context.researches.First(f => f.Id == entity.Id);
                entity.MapTo(v);
                _context.SaveChanges();
                return entity;
            }
        }
    }
}
