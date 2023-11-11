using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DBMySQL.Models
{
    public class GuildSkillEffect : Base
    {
        [ForeignKey("GuildSkillId")]
        public int GuildSkillId { get; set; }
        public decimal Value { get; set; }
        public string Mask { get; set; } = string.Empty;
    }
}
