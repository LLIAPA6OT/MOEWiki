using MOEWiki.DB.Enums;
using MOEWiki.DB.Models.Interfaces;

namespace MOEWiki.DB.Models
{
    public class Property : BaseName
    {
        public int AccessLvl { get; set; } = 0;
        public int SortId { get; set; }
        public PropertyTypeEnum PropertyType { get; set; }
        public List<ItemProperty> ItemProperties { get; set; } = new();
        public bool IsDependsByQuality { get; set; } = false;
    }
}
