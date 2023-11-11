using Microsoft.CodeAnalysis.Text;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;
using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MoeWiki.WebApp.Models
{
    public class MapMarkerModel : BaseName
    {
        public MapMarkerModel()
        {
        }
        public MapMarkerModel(int id)
        {
            this.Id = id;
            this.Init();
        }
        public MapMarkerModel(MapMarker entity)
        {
            this.Init(entity);
        }

        public void Init()
        {
            this.Init(GatewaysFacade.MapMarkerGateway.GetById(this.Id));
        }


        private void Init(MapMarker entity)
        {
            entity.MapTo(this);
        }

        public int MapId { get; set; }
        public MarkerTypeEnum MarkerType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string? Title { get; set; } = String.Empty;
        public string ImageName { get; set; } = String.Empty;
        public string? Description { get; set; } = String.Empty;
        public string? Enemies { get; set; } = String.Empty;
        public string? Link { get; set; } = String.Empty;
        public string? LinkName { get; set; } = String.Empty;
        public CoalitionEnum Coalition { get; set; }
        public string? Note { get; set; } = String.Empty;
    }
}
