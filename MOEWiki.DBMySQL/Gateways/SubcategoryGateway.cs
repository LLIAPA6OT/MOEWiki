using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class SubcategoryGateway : BaseGateway
    {
        public SubcategoryGateway(string connectionString) : base(connectionString) { }

        public Subcategory GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.subcategories.AsNoTracking().First(f => f.Id == id);
        }
        public Subcategory? GetByNameOrSynonym(string name)
        {
            using (var _context = new ApplicationContext(options))
                return _context.subcategories.AsNoTracking().FirstOrDefault(f => f.Name == name || f.Synonyms.Contains(name));
        }
        public List<Subcategory> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.subcategories.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<Subcategory> GetAllIncludeCategory()
        {
            using (var _context = new ApplicationContext(options))
                return _context.subcategories.AsNoTracking().Where(w => !w.IsDelete).Include(i => i.Category).ToList();
        }
        public List<Subcategory> GetAllWithDeletedAndNotActual()
        {
            using (var _context = new ApplicationContext(options))
                return _context.subcategories.AsNoTracking().ToList();
        }
        public Subcategory Add(Subcategory entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.subcategories.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public Subcategory Update(Subcategory entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                var v = _context.subcategories.First(f => f.Id == entity.Id);
                entity.MapTo(v);
                _context.SaveChanges();
                return entity;
            }
        }
        public void Delete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var list = GatewaysFacade.ItemGateway.GetAll().Where(w => w.SubcategoryId == id);
                if (list.Any()) { throw new Exception($"Есть {list.Count()} связанных предметов. Удаление не возможно!"); }
                var entity = _context.subcategories.First(f => f.Id == id);
                entity.IsDelete = true;
                _context.SaveChanges();
            }
        }
    }
}
