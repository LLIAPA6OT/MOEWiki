using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;
using System.Reflection.Metadata;

namespace MOEWiki.DBMySQL.Gateways
{
    public class MapGateway : BaseGateway
    {
        public MapGateway(string connectionString) : base(connectionString) { }

        public Map GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.maps.AsNoTracking().First(f => f.Id == id);
        }
        public Map GetByIdIncludeZoneAndMarker(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.maps.AsNoTracking().Include(i => i.MapMarkers).Include(i => i.MapZones).First(f => f.Id == id);
        }
        public List<Map> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.maps.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
    }
}
