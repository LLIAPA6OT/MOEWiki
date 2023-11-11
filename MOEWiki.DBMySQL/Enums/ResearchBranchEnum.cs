using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEWiki.DBMySQL.Enums
{
    public enum ResearchBranchEnum
    {
        [Description("Workbench and Tools")]
        Workbench = 0,
        [Description("Building and Furniture")]
        Building = 1,
        [Description("Weapon")]
        Weapon = 2,
        [Description("Armor")]
        Armor = 3,
        [Description("Tame and Recruit")]
        Tame = 4,
        [Description("Food and Medicine")]
        FoodAndMed = 5,
        [Description("Cart and Cage")]
        Cart = 6,
        [Description("Decorative Building")]
        Decorative = 7,
        [Description("Guild Tech")]
        Guild = 8,
        [Description("Trap Building")]
        Trap = 9
    }
}
