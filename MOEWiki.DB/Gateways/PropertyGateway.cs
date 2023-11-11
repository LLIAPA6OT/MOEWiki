using Microsoft.EntityFrameworkCore;
using MOEWiki.DB.Models;

namespace MOEWiki.DB.Gateways
{
    public class PropertyGateway : BaseGateway
    {
        public PropertyGateway(ApplicationContext context) : base(context) { }

        public Property GetById(int id)
        {
            return _context.Properties.AsNoTracking().First(f => f.Id == id);
        }
        private Property GetByIdTracking(int id)
        {
            return _context.Properties.First(f => f.Id == id);
        }
        public IQueryable<Property> GetAll()
        {
            return _context.Properties.AsNoTracking().Where(w => !w.IsDelete);
        }
        public IQueryable<Property> GetAllWithDeletedAndNotActual()
        {
            return _context.Properties.AsNoTracking();
        }
        public Property Add(Property entity)
        {
            _context.Properties.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public Property Update(Property entity)
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
        public void HardDelete(int id)
        {            
            foreach (var property in _context.ItemProperties.Where(w => w.PropertyId == id))
            {
                _context.ItemProperties.Remove(property);
            }            
            foreach (var property in _context.ItemPropertiesWithNonActual.Where(w => w.PropertyId == id))
            {
                _context.ItemPropertiesWithNonActual.Remove(property);
            }
            var entity = GetByIdTracking(id);
            _context.Properties.Remove(entity);
            _context.SaveChanges();
        }
    }
}
