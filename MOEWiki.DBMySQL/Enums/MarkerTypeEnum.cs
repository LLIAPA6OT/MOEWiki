using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEWiki.DBMySQL.Enums
{
    public enum MarkerTypeEnum
    {
        [Description("Strongholds")]
        Strongholds = 100,
        [Description("Mines")]
        Mines = 200,
        [Description("Fields(Collects)")]
        Fields = 300,
        [Description("Animals")]
        Animals = 400,
        [Description("Objects")]
        Objects = 500,
        [Description("Elite")]
        Elite = 600,
        [Description("Levels")]
        Levels = 700,
        [Description("Coalitions")]
        Coalition = 800,
    }
}
