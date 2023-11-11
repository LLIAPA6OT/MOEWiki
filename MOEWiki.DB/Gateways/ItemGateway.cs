using Microsoft.EntityFrameworkCore;
using MOEWiki.DB.Models;

namespace MOEWiki.DB.Gateways
{
    public class ItemGateway : BaseGateway
    {
        public ItemGateway(ApplicationContext context) : base(context) { }

        public Item GetById(int id)
        {
            return _context.Items.AsNoTracking().First(f => f.Id == id);
        }
        public Item GetByNameSmart(string name)
        {
            return _context.Items.AsNoTracking().FirstOrDefault(f => f.Name.Trim().ToLower().Replace(" ", "") == name.Trim().ToLower().Replace(" ", ""));
        }
        private Item GetByIdTracking(int id)
        {
            return _context.Items.First(f => f.Id == id);
        }
        public IQueryable<Item> GetAll()
        {
            return _context.Items.AsNoTracking().Where(w => !w.IsDelete);
        }
        public IQueryable<Item> GetAllMaterials()
        {
            var result = _context.Items.AsNoTracking().Where(w => !w.IsDelete).OrderBy(o => o.Name);
            var materialCategory = _context.Categories.AsNoTracking().Include(i => i.Subcategories).FirstOrDefault(w => w.Name == "Material");
            if (materialCategory != null)
            {
                var subcats = materialCategory.Subcategories.Select(w => w.Id).ToList();
                result = result.Where(w => subcats.Contains(w.SubcategoryId)).OrderBy(o => o.Name);
            }
            return result;
        }
        public IQueryable<Item> GetAllIncludeSubcat()
        {
            return _context.Items.AsNoTracking().Where(w => !w.IsDelete).Include(i => i.Subcategory);
        }
        public IQueryable<Item> GetAllWithDeletedAndNotActual()
        {
            return _context.Items.AsNoTracking();
        }
        public Item Add(Item entity)
        {
            _context.Items.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public Item Update(Item entity)
        {
            var v = GetByIdTracking(entity.Id);
            entity.MapTo(v);
            _context.SaveChanges();
            return entity;
        }
        public void Delete(int id)
        {
            var entity = GetByIdTracking(id);
            entity.IsDelete = true;
            _context.SaveChanges();
        }
        public bool CheckImageName(string imagename)
        {
            return _context.Items.Any(a => a.ImageName == imagename);
        }
        public IQueryable<Item> SearchByName(string name)
        {
            return _context.Items.AsNoTracking().Where(w => w.Name.Contains(name.Trim()));
        }
    }
}
