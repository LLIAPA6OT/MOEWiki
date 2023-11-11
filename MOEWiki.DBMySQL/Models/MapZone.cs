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
    public class MapZone : BaseName
    {
        public int MapId { get; set; }
        [ForeignKey("MapId")]
        public Map? Map { get; set; }
        public int iX { get; set; }
        public int iY { get; set; }
        public int aX { get; set; }
        public int aY { get; set; }
        public string ImageName { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public bool IsHidden { get; set; } = false;
    }
}
