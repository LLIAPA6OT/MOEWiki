using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class MarkerGroupGateway : BaseGateway
    {
        public MarkerGroupGateway(string connectionString) : base(connectionString) { }

        public MarkerGroup GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.markergroups.AsNoTracking().First(f => f.Id == id);
        }
        public MarkerGroup GetByIdIncludeMarkersAndMap(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.markergroups.AsNoTracking().Include(i => i.MapMarkers).Include(i => i.Map).First(f => f.Id == id);
        }
        public List<MarkerGroup> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.markergroups.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<MarkerGroup> GetAllByMapId(int mapId)
        {
            using (var _context = new ApplicationContext(options))
                return _context.markergroups.AsNoTracking().Where(w => !w.IsDelete && !w.IsHidden && w.MapId == mapId).ToList();
        }
    }
}
