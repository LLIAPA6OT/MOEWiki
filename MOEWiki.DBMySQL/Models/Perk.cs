using EnumsNET;
using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DBMySQL.Models
{
    public class Perk : BaseName
    {
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public SkillEnum Skill { get; set; }
        public int? PreviousId { get; set; }
        public string? PreviousName { get; set; }
        public int Level { get; set; }
        public int SkillLevel { get; set; }
        public int ComprehensionPoint { get; set; }
        public int CopperCoins { get; set; }
        public int RequiredItemId { get; set; }
        public string? RequiredItemName { get; set; }
        public int SortId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Row { get; set; }
    }
}
