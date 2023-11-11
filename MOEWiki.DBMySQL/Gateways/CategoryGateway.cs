using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class CategoryGateway
    {
        private readonly DbContextOptions<ApplicationContext> options;
        public CategoryGateway(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            options = optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0))).Options;
        }

        public Category GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.categories.AsNoTracking().First(f => f.Id == id);
        }
        public List<Category> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.categories.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<Category> GetAllIncludeSubcat()
        {
            using (var _context = new ApplicationContext(options))
                return _context.categories.AsNoTracking().Where(w => !w.IsDelete).Include(i => i.Subcategories).ToList();
        }
        public List<Category> GetAllWithDeletedAndNotActual()
        {
            using (var _context = new ApplicationContext(options))
                return _context.categories.AsNoTracking().ToList();
        }
        public Category Add(Category entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.categories.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public Category Update(Category entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                var v = _context.categories.First(f => f.Id == entity.Id);
                entity.MapTo(v);
                _context.SaveChanges();
                return entity;
            }
        }
        public void Delete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var list = GatewaysFacade.SubcategoryGateway.GetAll().Where(w => w.CategoryId == id);
                if (list.Any()) { throw new Exception($"Есть {list.Count()} связанных подкатегорий. Удаление не возможно!"); }
                var entity = _context.categories.First(f => f.Id == id);
                entity.IsDelete = true;
                _context.SaveChanges();
            }
        }
    }
}
