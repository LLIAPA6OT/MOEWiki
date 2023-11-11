using EnumsNET;
using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MOEWiki.DBMySQL.Gateways
{
    public class MapMarkerGateway : BaseGateway
    {
        public MapMarkerGateway(string connectionString) : base(connectionString) { }

        public MapMarker GetById(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.mapmarkers.AsNoTracking().First(f => f.Id == id);
        }
        public List<MapMarker> GetAll()
        {
            using (var _context = new ApplicationContext(options))
                return _context.mapmarkers.AsNoTracking().Where(w => !w.IsDelete).ToList();
        }
        public List<MapMarker> GetAllByMapId(int mapId)
        {
            using (var _context = new ApplicationContext(options))
                return _context.mapmarkers.AsNoTracking().Where(w => !w.IsDelete && !w.IsHidden && w.MapId == mapId).ToList();
        }
        public List<MapMarker> GenerateMarkers(List<MapMarker> markers)
        {
            var result = new List<MapMarker>();
            foreach (var m in markers.Where(w => w.Coalition != CoalitionEnum.None && w.MarkerType != MarkerTypeEnum.Elite))
            {
                var c = m.Coalition.AsString(EnumFormat.Description);
                result.Add(new MapMarker() { Name = c + " Coalition", Title = c, X = m.X, Y = m.Y, ImageName = c + "Coalition.png", MarkerType = MarkerTypeEnum.Coalition, SortId = 50, Enemies = m.Enemies});
            }
            foreach (var m in markers.Where(w => !string.IsNullOrWhiteSpace(w.Enemies) && w.MarkerType != MarkerTypeEnum.Elite))
            {
                var k = m.Enemies.IndexOf(']');
                if (k == -1) { continue; }
                var c = m.Enemies.Substring(k - 2, 2).Replace("-","").Trim();
                result.Add(new MapMarker() { Name = "Max " + c, Title = m.Enemies.Substring(0,k+1), X = m.X, Y = m.Y, ImageName = c + ".png", MarkerType = MarkerTypeEnum.Levels, SortId = 500 + int.Parse(c), Enemies = m.Enemies });
            }
            return result;
        }
        public List<MapMarker> GetAllByMarkerGroupId(int id)
        {
            using (var _context = new ApplicationContext(options))
                return _context.mapmarkertomarkergrouprels.AsNoTracking().Where(w => w.MarkerGroupId == id).Include(w => w.MapMarker).Select(w => w.MapMarker).ToList();
        }
        public List<MapMarker> GetAllByMapIdAndType(int mapId, MarkerTypeEnum type)
        {
            using (var _context = new ApplicationContext(options))
                return GetAllByMapId(mapId).Where(w => w.MarkerType == type).ToList();
        }
        public MapMarker Add(MapMarker entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                _context.mapmarkers.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
        public MapMarker Update(MapMarker entity)
        {
            using (var _context = new ApplicationContext(options))
            {
                var v = _context.mapmarkers.First(f => f.Id == entity.Id);
                entity.MapTo(v);
                _context.SaveChanges();
                return entity;
            }
        }
        public void Delete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var entity = _context.mapmarkers.First(f => f.Id == id);
                entity.IsDelete = true;
                _context.SaveChanges();
            }
        }
        public void HardDelete(int id)
        {
            using (var _context = new ApplicationContext(options))
            {
                var entity = _context.mapmarkers.First(f => f.Id == id);
                _context.mapmarkers.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
