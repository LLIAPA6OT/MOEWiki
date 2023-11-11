using MOEWiki.DBMySQL.Gateways;
using MoeWiki.WebApp.Models.Item;

namespace MoeWiki.WebApp.Helpers
{
    public static class CalculatorHelper
    {
        public static int GetGuildActRec(int lvl)
        {
            var result = 0;
            if (lvl > 1)
            {
                result += 15735;
            }
            if (lvl > 2)
            {
                result += 155920;
            }
            if (lvl > 3)
            {
                result += 511900;
            }
            if (lvl > 4)
            {
                result += 7077470;
            }
            if (lvl > 5)
            {
                result += 8257048;
            }
            if (lvl > 6)
            {
                result += 9436627;
            }
            if (lvl > 7)
            {
                result += 29810367;
            }
            if (lvl > 8)
            {
                result += 33122630;
            }
            return result;
        }


        public static double GetLevelExp(int have, int target)
        {
            var arr = new double[60] { 0,100,136.6,181.5,236.1,302.4,382.6,479.1,595,733.9,899.8,914.7,1110.8,1343.7,1619.8,1946.5,2332.8,2788.7,3326.1,3958.9,4703.1,4648,5291.8,6024.7,6859.1,7809.1,8890.6,10122,11523.9,13120,14937.1,14171.5,16134.3,18368.9,20913,23809.5,27107.1,30861.4,35135.7,40002,45542.3,43208.2,49192.6,56005.7,63762.5,72593.6,82647.9,94094.6,107126.7,121963.7,138855.7,131739.4,149985.2,170758,194408.2,221333.8,251988.5,286888.9,326623,371860.3};
            return arr[have..target].Sum();
        }
        public static double GetSkillExp(int have, int target)
        {
            var arr = new double[13] { 0,400,1141,3809,7998,20789,33620,77309,124878,286984,463598,1065260,1720807};
            return arr[(have / 75 + 1)..(target / 75 + 1)].Sum();
        }

        public static List<RecipeItem> GetPrimitive(List<RecipeItem> list)
        {
            var result = new List<RecipeItem>();
            foreach (var item in list)
            {
                var newItems = GatewaysFacade.ItemRecipeGateway.GetAllByItemId(item.Id).Select(s => new RecipeItem() { Id = s.RecipeItemId, Name = s.RecipeItemName, Count = s.Count * item.Count, Number = s.Number + s.ItemId * 1000, IsStepByStep = s.IsStepByStep }).ToList();
                if (newItems.Any())
                {
                    result.AddRange(GetPrimitive(newItems));
                    item.IsDeleted = true;
                }
            }
            list.AddRange(result);
            return list;
        }
    }
}
