using Microsoft.EntityFrameworkCore;
using MOEWiki.DB.Models;

namespace MOEWiki.DB.Gateways
{
    public class ItemRecipeGateway : BaseGateway
    {
        public ItemRecipeGateway(ApplicationContext context) : base(context) { }

        public ItemRecipe GetById(int id)
        {
            return _context.ItemRecipes.AsNoTracking().First(f => f.Id == id);
        }
        public IQueryable<ItemRecipe> GetAllByItemId(int id)
        {
            return _context.ItemRecipes.AsNoTracking().Where(f => f.ItemId == id);
        }
        private ItemRecipe GetByIdTracking(int id)
        {
            return _context.ItemRecipes.First(f => f.Id == id);
        }
        public IQueryable<ItemRecipe> GetAll()
        {
            return _context.ItemRecipes.AsNoTracking().Where(w => !w.IsDelete);
        }
        public IQueryable<ItemRecipe> GetAllWithDeletedAndNotActual()
        {
            return _context.ItemRecipes.AsNoTracking();
        }
        private void CreateWAN(ItemRecipe entity)
        {
            _context.ItemRecipesWithNonActual.Add((ItemRecipeWithNonActual)entity);
            _context.SaveChanges();
        }
        public ItemRecipe Add(ItemRecipe entity)
        {
            _context.ItemRecipes.Add(entity);
            _context.SaveChanges();
            CreateWAN(entity);
            return entity;
        }
        public ItemRecipe Update(ItemRecipe entity)
        {
            var v = GetByIdTracking(entity.Id);
            entity.MapTo(v);
            _context.SaveChanges();
            CreateWAN(entity);
            return entity;
        }
        private void CreateWANWhenDelete(ItemRecipe entity)
        {
            entity.IsDelete = true;
            entity.CreationDate = DateTime.Now;
            _context.ItemRecipesWithNonActual.Add((ItemRecipeWithNonActual)entity);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var entity = GetByIdTracking(id);
            CreateWANWhenDelete(entity);
            _context.Remove(entity);
            _context.SaveChanges();
        }
        public void HardDelete(int id)
        {
            foreach (var wan in _context.ItemRecipesWithNonActual.Where(x => x.ItemRecipeId == id))
            {
                _context.Remove(wan);
            }
            var entity = GetByIdTracking(id);
            _context.Remove(entity);
            _context.SaveChanges();
        }
    }
}
