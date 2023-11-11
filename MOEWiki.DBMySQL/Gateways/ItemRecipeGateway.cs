using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class ItemRecipeGateway : BaseGateway
    {
        public ItemRecipeGateway(string connectionString) : base(connectionString) { }

        public ItemRecipe GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.itemrecipes.AsNoTracking().First(f => f.Id == id);
        }
        public List<ItemRecipe> GetAllByItemId(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.itemrecipes.AsNoTracking().Where(f => f.ItemId == id).ToList();
        }
        public List<ItemRecipe> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.itemrecipes.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<ItemRecipe> GetAllWithDeletedAndNotActual()
        {
            using (var _context = new ApplicationContext(options))
                return _context.itemrecipes.AsNoTracking().ToList();
        }
        private void CreateWAN(ItemRecipe entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.itemrecipeswithnonactual.Add((ItemRecipeWithNonActual)entity);
                _context.SaveChanges();
            }
        }
        public ItemRecipe Add(ItemRecipe entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.itemrecipes.Add(entity);
                _context.SaveChanges();
                CreateWAN(entity);
                return entity;
            }
        }
        public ItemRecipe Update(ItemRecipe entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                var v = _context.itemrecipes.First(f => f.Id == entity.Id);
                entity.MapTo(v);
                _context.SaveChanges();
                CreateWAN(entity);
                return entity;
            }
        }
        private void CreateWANWhenDelete(ItemRecipe entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                entity.IsDelete = true;
                entity.CreationDate = DateTime.Now;
                _context.itemrecipeswithnonactual.Add((ItemRecipeWithNonActual)entity);
                _context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var entity = _context.itemrecipes.First(f => f.Id == id);
                CreateWANWhenDelete(entity);
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }
        public void HardDelete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                foreach (var wan in _context.itemrecipeswithnonactual.Where(x => x.ItemRecipeId == id))
                {
                    _context.Remove(wan);
                }
                var entity = _context.itemrecipes.First(f => f.Id == id);
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
