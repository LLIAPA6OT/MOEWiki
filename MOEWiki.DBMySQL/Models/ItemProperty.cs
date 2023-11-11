using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOEWiki.DBMySQL.Models
{
    public class ItemProperty : BaseName
    {
        public ItemProperty() { }
        public ItemProperty(Property property)
        {
            property.MapTo(this);
            this.PropertyId = property.Id;
            this.Id = 0;
            this.CreationDate = DateTime.Now;
        }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }
        public int AccessLvl { get; set; } = 0;
        public int SortId { get; set; }
        public PropertyTypeEnum PropertyType { get; set; }
        public string Value { get; set; } = string.Empty;
        public string HTMLValue { get; set; } = string.Empty;
        public QualityEnum Quality { get; set; } = QualityEnum.Low;
    }
}
