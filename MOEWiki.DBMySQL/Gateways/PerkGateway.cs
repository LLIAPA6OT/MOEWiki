using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class PerkGateway : BaseGateway
    {
        public PerkGateway(string connectionString) : base(connectionString) { }

        public Perk GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.perks.AsNoTracking().First(f => f.Id == id);
        }
        public Perk? GetByName(string name)
        {
            using (var _context = new ApplicationContext(options))
                return _context.perks.AsNoTracking().FirstOrDefault(f => f.Name == name);
        }
        public List<Perk> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.perks.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<Perk> GetAllWithDeletedAndNotActual()
        {
            using (var _context = new ApplicationContext(options))
                return _context.perks.AsNoTracking().ToList();
        }
        public Perk Add(Perk entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.perks.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public Perk Update(Perk entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                var v = _context.perks.First(f => f.Id == entity.Id);
                entity.MapTo(v);
                _context.SaveChanges();
                return entity;
            }
        }
        public void Delete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var entity = _context.perks.First(f => f.Id == id);
                entity.IsDelete = true;
                _context.SaveChanges();
            }
        }
        public void HardDelete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var entity = _context.perks.First(f => f.Id == id);
                _context.perks.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
