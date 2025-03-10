using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Models;

namespace MoeWiki.WebApp.Models.Map
{
    public class MapModel
    {
        public MapModel(MOEWiki.DBMySQL.Models.Map map)
        {
            this.Map = map;
            this.Type = (int)MapDisplayTypeEnum.Map;
            this.MapMarkers = GatewaysFacade.MapMarkerGateway.GetAllByMapId(map.Id);
            this.MapMarkers.AddRange(GatewaysFacade.MapMarkerGateway.GenerateMarkers(this.MapMarkers));
            this.MapMarkers = this.MapMarkers.OrderBy(o => o.SortId).ToList();
            this.MapZones = GatewaysFacade.MapZoneGateway.GetAllByMapId(map.Id);
        }
        public MapModel(MapZone mapZone)
        {
            this.MapZone = mapZone;
            this.MapZones = new List<MapZone>
            {
                mapZone
            };
            this.Map = GatewaysFacade.MapGateway.GetById(mapZone.MapId);
            this.Type = (int)MapDisplayTypeEnum.MapZone;
        }
        public MapModel(MarkerGroup markerGroup)
        {
            this.MarkerGroup = markerGroup;
            this.Map = GatewaysFacade.MapGateway.GetById(markerGroup.MapId);
            this.Type = (int)MapDisplayTypeEnum.MarkerGroup;
            this.MapMarkers = GatewaysFacade.MapMarkerGateway.GetAllByMarkerGroupId(markerGroup.Id);
            this.MapMarkers = markerGroup.MapMarkers.Where(w => !w.IsDelete && !w.IsHidden).ToList();
        }
        public MOEWiki.DBMySQL.Models.Map Map { get; set; }
        public int Type { get; set; } = 1; //1 - Map, 2 - MarkerGroup, 3 - MapZone
        public MapZone? MapZone { get; set; }
        public MarkerGroup? MarkerGroup { get; set; }
        public List<MapZone>? MapZones { get; set; }
        public List<MapMarker>? MapMarkers { get; set; }
    }

}
