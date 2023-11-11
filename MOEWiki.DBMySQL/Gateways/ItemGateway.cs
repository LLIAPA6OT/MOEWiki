using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class ItemGateway : BaseGateway
    {
        public ItemGateway(string connectionString) : base(connectionString) { }

        public Item GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.AsNoTracking().First(f => f.Id == id);
        }
        public int GetResearchLevel(int id, int? researchId)
        {
            using (var _context = new ApplicationContext(options))
            {
                if (researchId.HasValue)
                {
                    return _context.researches.AsNoTracking().First(f => f.Id == researchId.Value).Level;
                }
                else
                {
                    foreach (var ir in _context.itemrecipes.AsNoTracking().Where(w => w.ItemId == id).Include(i => i.Item).ToList())
                    {
                        if (ir.Item.ResearchId.HasValue)
                        {
                            return _context.researches.AsNoTracking().First(f => f.Id == ir.Item.ResearchId.Value).Level;
                        }
                    }
                }
                return 0;
            }
        }
        public Item GetByIdIncludeRecipes(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.AsNoTracking().Include(i => i.ItemRecipes).First(f => f.Id == id);
        }
        public Item GetByIdForDetails(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.AsNoTracking().Where(w => w.Id == id).Include(i => i.ItemProperties).ThenInclude(t => t.Property).Include(i => i.ItemRecipes).Include(i => i.Subcategory).ThenInclude(t => t.Category).First();
        }
        public Item? GetByNameSmart(string name)
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.AsNoTracking().FirstOrDefault(f => f.Name.Trim().ToLower().Replace(" ", "") == name.Trim().ToLower().Replace(" ", ""));
        }
        public IEnumerable<Item> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public IEnumerable<Item> GetAllByResearchId(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.AsNoTracking().Where(w => !w.IsDelete && w.ResearchId == id).ToList();
        }
        public IEnumerable<Item> GetAllBySubcategoryId(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.AsNoTracking().Where(w => !w.IsDelete && w.SubcategoryId == id).ToList();
        }
        public List<Item> GetAllMaterials()
        {
            using (var _context = new ApplicationContext(options))
            {
                var result = _context.items.AsNoTracking().Where(w => !w.IsDelete).OrderBy(o => o.Name).ToList();
                var materialCategory = _context.categories.AsNoTracking().Include(i => i.Subcategories).FirstOrDefault(w => w.Name == "Material");
                if (materialCategory != null)
                {
                    var subcats = materialCategory.Subcategories.Select(w => w.Id).ToList();
                    result = result.Where(w => subcats.Contains(w.SubcategoryId)).OrderBy(o => o.Name).ToList();
                }
                return result;
            }
        }
        public List<Item> GetAllIncludeSubcat()
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.AsNoTracking().Where(w => !w.IsDelete).Include(i => i.Subcategory).ToList();
        }
        public List<Item> GetAllWithDeletedAndNotActual()
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.AsNoTracking().ToList();
        }
        public Item Add(Item entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.items.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public Item Update(Item entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                var v = _context.items.First(f => f.Id == entity.Id);
                entity.MapTo(v);
                _context.SaveChanges();
                return entity;
            }
        }
        public void Delete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var entity = _context.items.First(f => f.Id == id);
                entity.IsDelete = true;
                _context.SaveChanges();
            }
        }
        public bool CheckImageName(string imagename)
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.Any(a => a.ImageName == imagename);
        }
        public List<Item> SearchByName(string name)
        {
            using (var _context = new ApplicationContext(options))
                return _context.items.AsNoTracking().Where(w => w.Name.Contains(name.Trim())).ToList();
        }
        public void UpdateResearch(int id, int? newValue)
        {
            using (var _context = new ApplicationContext(options))
            {
                var entity = _context.items.First(f => f.Id == id);
                entity.ResearchId = newValue;
                _context.SaveChanges();
            }
        }
    }
}
