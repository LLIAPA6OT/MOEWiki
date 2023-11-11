using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class ItemPropertyGateway : BaseGateway
    {
        public ItemPropertyGateway(string connectionString) : base(connectionString) { }

        public ItemProperty GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.itemproperties.AsNoTracking().First(f => f.Id == id);
        }
        public List<ItemProperty> GetAllByItemId(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.itemproperties.AsNoTracking().Where(f => f.ItemId == id).ToList();
        }
        public List<ItemProperty> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.itemproperties.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<ItemProperty> GetAllWithDeletedAndNotActual()
        {
            using (var _context = new ApplicationContext(options))
                return _context.itemproperties.AsNoTracking().ToList();
        }
        public ItemProperty Add(ItemProperty entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.itemproperties.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public ItemProperty Update(ItemProperty entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                var v = _context.itemproperties.First(f => f.Id == entity.Id);
                entity.MapTo(v);
                _context.SaveChanges();
                CreateWAN(entity);
                return entity;
            }
        }
        private void CreateWAN(ItemProperty entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.itempropertieswithnonactual.Add((ItemPropertyWithNonActual)entity);
                _context.SaveChanges();
            }
        }
        private void CreateWANWhenDelete(ItemProperty entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                entity.IsDelete = true;
                entity.CreationDate = DateTime.Now;
                _context.itempropertieswithnonactual.Add((ItemPropertyWithNonActual)entity);
                _context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var entity = _context.itemproperties.First(f => f.Id == id);
                CreateWANWhenDelete(entity);
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }
        public void HardDelete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                foreach (var wan in _context.itempropertieswithnonactual.Where(x => x.ItemPropertyId == id))
                {
                    _context.Remove(wan);
                }
                var entity = _context.itemproperties.First(f => f.Id == id);
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
