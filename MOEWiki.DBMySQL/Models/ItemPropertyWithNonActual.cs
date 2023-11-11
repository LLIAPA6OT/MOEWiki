using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace MOEWiki.DBMySQL.Models
{
    public class ItemPropertyWithNonActual : BaseName
    {

        public static implicit operator ItemPropertyWithNonActual(ItemProperty itemProperty)
        {
            var entity = new ItemPropertyWithNonActual();
            itemProperty.MapTo(entity);
            entity.ItemPropertyId = itemProperty.Id;
            entity.Id = 0;
            entity.CreationDate = DateTime.Now;
            return entity;
        }
        public int ItemId { get; set; }
        public int ItemPropertyId { get; set; }
        public int PropertyId { get; set; }
        public int AccessLvl { get; set; } = 0;
        public int SortId { get; set; }
        public string Value { get; set; } = string.Empty;
        public string HTMLValue { get; set; } = string.Empty;
    }
}
