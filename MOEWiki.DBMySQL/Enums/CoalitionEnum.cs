using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEWiki.DBMySQL.Enums
{
    public enum CoalitionEnum
    {
        [Description("None")]
        None = 0,
        [Description("Rebel")]
        Rebel = 100,
        [Description("Raider")]
        Raider = 200,
        [Description("Pirate")]
        Pirate = 300,
        [Description("Robber")]
        Robber = 400,
        [Description("Yellow Turban")]
        YellowTurban = 500,
        [Description("Vagrant")]
        Vagrant = 600,
    }
}
