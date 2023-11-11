using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DBMySQL.Models
{
    public class GuildSkillRequire : Base
    {
        [ForeignKey("GuildSkillId")]
        public int GuildSkillId { get; set; }
        public int Level { get; set; }
        public int GuildLevel { get; set; }
        public int GuildActivity { get; set; }
        public int CopperCoins { get; set; }
        public int PrevLevel { get; set; }
    }
}
