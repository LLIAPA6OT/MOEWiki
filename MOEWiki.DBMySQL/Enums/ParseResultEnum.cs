using System.ComponentModel;

namespace MOEWiki.DBMySQL.Enums
{
    public enum ParseResultEnum
    {
        ItemNotFound = 0,
        NothingChanged = 1,
        HaveNewItemProperties = 2,
        HaveNewItemRecipies = 3,
        HaveNewRecipeItems = 4,
        HaveItemPropertiesToUpdate = 6,
        HaveItemRecipiesToUpdate = 7,
        HaveItemPropertiesToDelete = 8,
        HaveItemRecipiesToDelete = 9,
        DescriptionChanged = 10,
        ImageChanged = 11,
        HaveNewPropertise = 12,

    }
}
