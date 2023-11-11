using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEWiki.DBMySQL.Enums
{
    public enum ModeEnum
    {
        [Description("Предмет")]
        Item = 0,
        [Description("Предмет + рецепт")]
        ItemPlusRecipe = 1,
        [Description("Рецепт")]
        Recipe = 2,
        [Description("Исследование")]
        Research = 3,
        [Description("Перк")]
        Perk = 4
    }
}
