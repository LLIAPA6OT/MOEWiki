using Microsoft.EntityFrameworkCore;
using MOEWiki.DB.Models;

namespace MOEWiki.DB.Gateways
{
    public class SubcategoryGateway : BaseGateway
    {
        public SubcategoryGateway(ApplicationContext context) : base(context) { }

        public Subcategory GetById(int id)
        {
            return _context.Subcategories.AsNoTracking().First(f => f.Id == id);
        }
        public Subcategory GetByNameOrSynonym(string name)
        {
            return _context.Subcategories.AsNoTracking().FirstOrDefault(f => f.Name == name || f.Synonyms.Contains(name));
        }
        private Subcategory GetByIdTracking(int id)
        {
            return _context.Subcategories.First(f => f.Id == id);
        }
        public IQueryable<Subcategory> GetAll()
        {
            return _context.Subcategories.AsNoTracking().Where(w => !w.IsDelete);
        }
        public IQueryable<Subcategory> GetAllIncludeCategory()
        {
            return _context.Subcategories.AsNoTracking().Where(w => !w.IsDelete).Include(i => i.Category);
        }
        public IQueryable<Subcategory> GetAllWithDeletedAndNotActual()
        {
            return _context.Subcategories.AsNoTracking();
        }
        public Subcategory Add(Subcategory entity)
        {
            _context.Subcategories.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public Subcategory Update(Subcategory entity)
        {
            var v = GetByIdTracking(entity.Id);
            entity.MapTo(v);
            _context.SaveChanges();
            return entity;
        }
        public void Delete(int id)
        {
            var list = GatewaysFacade.ItemGateway.GetAll().Where(w => w.SubcategoryId == id);
            if (list.Any()) { throw new Exception($"Есть {list.Count()} связанных предметов. Удаление не возможно!"); }
            var entity = GetByIdTracking(id);
            entity.IsDelete = true;
            _context.SaveChanges();
        }
    }
}
