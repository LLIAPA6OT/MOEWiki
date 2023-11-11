using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEWiki.DBMySQL.Enums
{
    public enum GuildSkillEnum
    {
        [Description("Guild - Battle")]
        GuildBattle = 9901,
        [Description("Guild - Production")]
        GuildProduction = 9902,
        [Description("Guild - Command")]
        GuildCommand = 9903,
        [Description("Guild - Internal Affairs")]
        GuildInternalAffairs = 9904,
    }
}
