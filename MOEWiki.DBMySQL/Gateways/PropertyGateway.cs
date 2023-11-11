using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class PropertyGateway : BaseGateway
    {
        public PropertyGateway(string connectionString) : base(connectionString) { }

        public Property GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.properties.AsNoTracking().First(f => f.Id == id);
        }
        public List<Property> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.properties.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<Property> GetAllWithDeletedAndNotActual()
        {
            using (var _context = new ApplicationContext(options))
                return _context.properties.AsNoTracking().ToList();
        }
        public Property Add(Property entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.properties.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public Property Update(Property entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                var v = _context.properties.First(f => f.Id == entity.Id);
                entity.MapTo(v);
                _context.SaveChanges();
                return entity;
            }
        }
        public void Delete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var entity = _context.properties.First(f => f.Id == id);
                entity.IsDelete = true;
                _context.SaveChanges();
            }
        }
        public void HardDelete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                foreach (var property in _context.itemproperties.Where(w => w.PropertyId == id))
                {
                    _context.itemproperties.Remove(property);
                }
                foreach (var property in _context.itempropertieswithnonactual.Where(w => w.PropertyId == id))
                {
                    _context.itempropertieswithnonactual.Remove(property);
                }
                var entity = _context.properties.First(f => f.Id == id);
                _context.properties.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
