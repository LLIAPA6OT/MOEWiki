using System.ComponentModel;

namespace MOEWiki.DBMySQL.Enums
{
    public enum ParseStateEnum
    {
        Scanned = 0,
        Recognized = 1,
        Parsed = 2,
        Confirmed = 3,
        Returned = 4,
    }
}
