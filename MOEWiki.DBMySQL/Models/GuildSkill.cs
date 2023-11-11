using Microsoft.EntityFrameworkCore;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace MOEWiki.DBMySQL.Models
{
    public class GuildSkill : BaseName
    {
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public GuildSkillEnum Skill { get; set; }
        public int MaxLevel { get; set; }
        public int SortId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Row { get; set; }
        public int Column { get; set; }
        public List<GuildSkillEffect> GuildSkillEffects { get; set; } = new();
        public List<GuildSkillRequire> GuildSkillRequires { get; set; } = new();
        public List<GuildSkillRelation> GuildSkillRelations { get; set; } = new();
        [NotMapped]
        public PrevRequiresTypeEnum PrevRequiresType
        {
            get
            {
                if (this.GuildSkillRequires == null || !this.GuildSkillRequires.Any()) return PrevRequiresTypeEnum.ByZiro;
                var distinct = this.GuildSkillRequires.Select(s => s.PrevLevel).Distinct();
                if (distinct.Count() == 1)
                {
                    return (PrevRequiresTypeEnum)distinct.First();
                } else if (distinct.Count() == this.MaxLevel)
                {
                    var f = GuildSkillRequires.First();
                    if (f.PrevLevel == f.Level * 5 + 5)
                        return PrevRequiresTypeEnum.FivePlusOne;
                    else if (f.PrevLevel == f.Level * 2)
                        return PrevRequiresTypeEnum.Equal2;
                    else if (f.PrevLevel == f.Level * 10)
                        return PrevRequiresTypeEnum.Equal10;
                    else if (f.PrevLevel == f.Level)
                        return PrevRequiresTypeEnum.Equal1;
                    else return PrevRequiresTypeEnum.Spec47;
                }
                else if (distinct.Count() * 2 == this.MaxLevel)
                {
                    return PrevRequiresTypeEnum.TwiseTen;
                }
                else return PrevRequiresTypeEnum.Spec66;
            }
        }
    }
}
