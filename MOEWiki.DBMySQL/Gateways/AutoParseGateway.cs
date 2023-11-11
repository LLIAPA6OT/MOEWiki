using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class AutoParseGateway : BaseGateway
    {
        public AutoParseGateway(string connectionString) : base(connectionString) { }

        public AutoParse GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.autoparses.AsNoTracking().First(f => f.Id == id);
        }
        public List<AutoParse> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.autoparses.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<AutoParse> GetAllParsed()
        {
            using (var _context = new ApplicationContext(options))
                return _context.autoparses.AsNoTracking().Where(w => !w.IsDelete && w.ParseState == Enums.ParseStateEnum.Parsed).ToList();
        }
        public List<AutoParse> GetAllWithDeletedAndNotActual()
        {
            using (var _context = new ApplicationContext(options))
                return _context.autoparses.AsNoTracking().ToList();
        }
        public AutoParse Add(AutoParse entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.autoparses.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public AutoParse Update(AutoParse entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                var v = _context.autoparses.First(f => f.Id == entity.Id);
                entity.MapTo(v);
                _context.SaveChanges();
                return entity;
            }
        }
        public void Delete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var entity = _context.autoparses.First(f => f.Id == id);
                entity.IsDelete = true;
                _context.SaveChanges();
            }
        }
        public bool CheckImageName(string imagename)
        {
            using (var _context = new ApplicationContext(options))            
                return _context.autoparses.Any(a => a.ImageName == imagename);
        }
    }
}
