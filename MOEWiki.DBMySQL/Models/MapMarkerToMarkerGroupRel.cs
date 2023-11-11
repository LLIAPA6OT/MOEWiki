using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DBMySQL.Models
{
    public class MapMarkerToMarkerGroupRel : Base
    {
        public int MarkerGroupId { get; set; }
        [ForeignKey("MarkerGroupId")]
        public MarkerGroup? MarkerGroup { get; set; }
        public int MapMarkerId { get; set; }
        [ForeignKey("MapMarkerId")]
        public MapMarker? MapMarker { get; set; }
        public string SpecialInfo { get; set; } = String.Empty;
    }
}
