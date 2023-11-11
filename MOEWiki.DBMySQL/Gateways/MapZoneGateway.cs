using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class MapZoneGateway : BaseGateway
    {
        public MapZoneGateway(string connectionString) : base(connectionString) { }

        public MapZone GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.mapzones.AsNoTracking().First(f => f.Id == id);
        }
        public MapZone GetByIdIncludeMap(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.mapzones.AsNoTracking().Include(i => i.Map).First(f => f.Id == id);
        }
        public List<MapZone> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.mapzones.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<MapZone> GetAllByMapId(int mapId)
        {
            using (var _context = new ApplicationContext(options))
                return _context.mapzones.AsNoTracking().Where(w => !w.IsDelete && !w.IsHidden && w.MapId == mapId).ToList();
        }
    }
}
