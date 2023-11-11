using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEWiki.DBMySQL.Enums
{
    public enum MapDisplayTypeEnum
    {
        [Description("Map")]
        Map = 1,
        [Description("MarkerGroup")]
        MarkerGroup = 2,
        [Description("MapZone")]
        MapZone = 3
    }
}
