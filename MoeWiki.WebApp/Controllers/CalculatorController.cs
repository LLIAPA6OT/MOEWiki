using Microsoft.AspNetCore.Mvc;
using MoeWiki.WebApp.Helpers;
using MoeWiki.WebApp.Models.Calculator;
using MoeWiki.WebApp.Models.GuildSkill;
using MoeWiki.WebApp.Models.GuildTech;
using MoeWiki.WebApp.Models.Home;
using MoeWiki.WebApp.Models.Item;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Models;
using System.Globalization;

namespace MoeWiki.WebApp.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult CraftItem(int id)
        {
            return View(GatewaysFacade.ItemGateway.GetByIdIncludeRecipes(id));
        }
        public IActionResult Field()
        {
            return View();
        }
        public IActionResult CraftIndex()
        {
            return View("\\Views\\Calculator\\CraftIndex.cshtml");
        }

        public IActionResult FindItem(string searchString)
        {
            ViewBag.SearchString = searchString;
            var model = new List<Item>();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                model = GatewaysFacade.ItemGateway.GetAll().Where(w => w.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(model);
        }
        public IActionResult Level()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CalculateLevel(int have, int target)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"LvlCalc");
            var model = new List<ReqItem>();
            double result = 0;
            if (have < target && have > 0 && have < 60 && target > 1 && target <= 60)
            {
                result = CalculatorHelper.GetLevelExp(have, target);
            }
            return Json(new { Result = result.ToString("N1", CultureInfo.CreateSpecificCulture("sv-SE")) });
        }

        public IActionResult Skill()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CalculateSkill(int have, int target)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"SkillCalc");
            var model = new List<ReqItem>();
            double result = 0;
            if (have < target && have >= 0 && have < 900 && target > 0 && target <= 900)
            {
                result = CalculatorHelper.GetSkillExp(have, target);
            }
            return Json(new { Result = result.ToString("N0", CultureInfo.CreateSpecificCulture("sv-SE")) });
        }
        [HttpPost]
        public IActionResult CalculateCraft(int id, int count)
        {
            var entity = GatewaysFacade.ItemGateway.GetByIdIncludeRecipes(id);
            var model = new CraftItemResultModel();
            if (entity != null && count > 0) 
            { 
                foreach (var item in entity.ItemRecipes)
                {
                    item.Count = item.Count*count;
                    model.costItems.Add(item);
                }
                var raw = CalculatorHelper.GetPrimitive(new List<RecipeItem>() { new RecipeItem() { Id = entity.Id, Name = entity.Name, Count = count, Number = 1 } });
                foreach (var group in raw.Where(w => !w.IsDeleted).GroupBy(g => g.Id))
                {
                    model.primitiveItems.Add(new RecipeItem() { Id = group.Key, Name = group.First().Name, Number = group.First().Number, IsStepByStep = group.First().IsStepByStep, Count = group.Sum(s => s.Count) });
                }
            }
            return View("~/Views/Calculator/_CraftResult.cshtml", model);
        }
        
    }
}
