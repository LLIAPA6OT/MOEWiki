using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOEWiki.DBMySQL.Enums
{
    public enum SkillEnum
    {
        [Description("One-handed")]
        OneHanded = 101,
        [Description("Shield")]
        Shield = 102,
        [Description("Two-handed")]
        TwoHanded = 103,
        [Description("Polearm")]
        Polearm = 104,
        [Description("Heavy Armor")]
        HeavyArmor = 105,
        [Description("Projectile")]
        Projectile = 201,
        [Description("Bow")]
        Bow = 202,
        [Description("Crossbow")]
        Crossbow = 203,
        [Description("Riding")]
        Riding = 204,
        [Description("Light Armor")]
        LightArmor = 205,
        [Description("Physique")]
        Physique = 301,
        [Description("Mining")]
        Mining = 302,
        [Description("Lumbering")]
        Lumbering = 303,
        [Description("Hunt")]
        Hunt = 304,
        [Description("Hunt")]
        Plant = 305,
        [Description("Craft")]
        Craft = 401,
        [Description("Siege")]
        Siege = 402,
        [Description("Building")]
        Building = 403,
        [Description("Armorer")]
        Armorer = 404,
        [Description("Medicine")]
        Medicine = 405,
        [Description("Command")]
        Command = 501,
        [Description("Drills")]
        Drills = 502,
        [Description("Recruitment")]
        Recruitment = 503,
        [Description("Tame")]
        Tame = 504,
        [Description("Renown")]
        Renown = 505,
    }
}
