using EnumsNET;
using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DBMySQL.Models
{
    public class MapMarker : BaseName
    {
        public int MapId { get; set; }
        [ForeignKey("MapId")]
        public Map? Map { get; set; }
        public MarkerTypeEnum MarkerType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int SortId { get; set; }
        public bool IsHidden { get; set; } = false;
        public string Title { get; set; } = String.Empty;
        public string ImageName { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Enemies { get; set; } = String.Empty;
        public string Link { get; set; } = String.Empty;
        public string LinkName { get; set; } = String.Empty;
        public CoalitionEnum Coalition { get; set; }
        public string Note { get; set; } = String.Empty;
        [NotMapped]
        public string Popup
        {
            get
            {
                //var result = $"<div><b>{this.Id}|{this.Name}|</b><a href='/MapMarker/Edit/?id={this.Id}' target=\"_blank\">Edit</a>";
                var result = $"<div><b>{this.Title}</b>";
                if (!string.IsNullOrWhiteSpace(this.Description))
                {
                    result += $"<hr><p>{this.Description}</p>";
                }
                if (!string.IsNullOrWhiteSpace(this.Link))
                {
                    result += $"<a href '{this.Link}'>{this.LinkName}</a>";
                }
                if (this.Coalition != CoalitionEnum.None)
                {
                    result += $"<hr><p>{this.Coalition.AsString(EnumFormat.Description)}</p>";
                }
                if (!string.IsNullOrWhiteSpace(this.Enemies))
                {
                    result += $"<p>{this.Enemies}</p>";
                }
                return result + "</div>";
            }
        }
    }
}
