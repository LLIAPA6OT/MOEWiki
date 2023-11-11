using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DBMySQL.Models
{
    public class MarkerGroup : BaseName
    {
        public int MapId { get; set; }
        [ForeignKey("MapId")]
        public Map? Map { get; set; }
        public string Description { get; set; } = String.Empty;
        public List<MapMarker> MapMarkers { get; set; } = new();
        public bool IsHidden { get; set; } = false;
    }
}
