using Microsoft.EntityFrameworkCore;
using MOEWiki.DB.Models;

namespace MOEWiki.DB.Gateways
{
    public class ItemPropertyGateway : BaseGateway
    {
        public ItemPropertyGateway(ApplicationContext context) : base(context) { }

        public ItemProperty GetById(int id)
        {
            return _context.ItemProperties.AsNoTracking().First(f => f.Id == id);
        }
        public IQueryable<ItemProperty> GetAllByItemId(int id)
        {
            return _context.ItemProperties.AsNoTracking().Where(f => f.ItemId == id);
        }
        private ItemProperty GetByIdTracking(int id)
        {
            return _context.ItemProperties.First(f => f.Id == id);
        }
        public IQueryable<ItemProperty> GetAll()
        {
            return _context.ItemProperties.AsNoTracking().Where(w => !w.IsDelete);
        }
        public IQueryable<ItemProperty> GetAllWithDeletedAndNotActual()
        {
            return _context.ItemProperties.AsNoTracking();
        }
        public ItemProperty Add(ItemProperty entity)
        {
            _context.ItemProperties.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public ItemProperty Update(ItemProperty entity)
        {
            var v = GetByIdTracking(entity.Id);
            entity.MapTo(v);
            _context.SaveChanges();
            CreateWAN(entity);
            return entity;
        }
        private void CreateWAN(ItemProperty entity)
        {
            _context.ItemPropertiesWithNonActual.Add((ItemPropertyWithNonActual)entity);
            _context.SaveChanges();
        }
        private void CreateWANWhenDelete(ItemProperty entity)
        {
            entity.IsDelete = true;
            entity.CreationDate = DateTime.Now;
            _context.ItemPropertiesWithNonActual.Add((ItemPropertyWithNonActual)entity);
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
            foreach (var wan in _context.ItemPropertiesWithNonActual.Where(x => x.ItemPropertyId == id))
            {
                _context.Remove(wan);
            }
            var entity = GetByIdTracking(id);
            _context.Remove(entity);
            _context.SaveChanges();
        }
    }
}
