using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DBMySQL.Models
{
    [Index("Name")]
    public class Research : BaseName
    {
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public ResearchBranchEnum ResearchBranch { get; set; }
        public int? PreviousId { get; set; }
        public string? PreviousName { get; set; }
        public int Level { get; set; }
        public int RecipePoint { get; set; }
        public int? RequiredItemId { get; set; }
        public string? RequiredItemName { get; set; }
        public int? RequiredItemCount { get; set; }
        public int SortId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Row { get; set; }
        public int Column { get; set; }
        public int GuildLevel { get; set; }
        public int GuildActivity{ get; set; }
        public int CopperCoins { get; set; }
        public List<Item> Items { get; set; }
        [NotMapped]
        public List<Item> ItemsList { get; set; } = new List<Item>();
    }
}
