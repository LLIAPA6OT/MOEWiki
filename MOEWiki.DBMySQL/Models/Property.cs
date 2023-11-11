using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Models.Interfaces;

namespace MOEWiki.DBMySQL.Models
{
    public class Property : BaseName
    {
        public int AccessLvl { get; set; } = 0;
        public int SortId { get; set; }
        public PropertyTypeEnum PropertyType { get; set; }
        public List<ItemProperty> ItemProperties { get; set; } = new();
        public bool IsDependsByQuality { get; set; } = false;
        public bool IsHide { get; set; } = false;
    }
}
