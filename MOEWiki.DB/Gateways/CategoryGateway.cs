using Microsoft.EntityFrameworkCore;
using MOEWiki.DB.Models;

namespace MOEWiki.DB.Gateways
{
    public class CategoryGateway : BaseGateway
    {
        public CategoryGateway(ApplicationContext context) : base(context) { }

        public Category GetById(int id)
        {
            return _context.Categories.AsNoTracking().First(f => f.Id == id);
        }
        private Category GetByIdTracking(int id)
        {
            return _context.Categories.First(f => f.Id == id);
        }
        public IQueryable<Category> GetAll()
        {
            return _context.Categories.AsNoTracking().Where(w => !w.IsDelete);
        }
        public IQueryable<Category> GetAllWithDeletedAndNotActual()
        {
            return _context.Categories.AsNoTracking();
        }
        public Category Add(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public Category Update(Category entity)
        {
            var v = GetByIdTracking(entity.Id);
            entity.MapTo(v);
            _context.SaveChanges();
            return entity;
        }
        public void Delete(int id)
        {
            var list = GatewaysFacade.SubcategoryGateway.GetAll().Where(w => w.CategoryId == id);
            if (list.Any()) { throw new Exception($"Есть {list.Count()} связанных подкатегорий. Удаление не возможно!"); }
            var entity = GetByIdTracking(id);
            entity.IsDelete = true;
            _context.SaveChanges();
        }
    }
}
