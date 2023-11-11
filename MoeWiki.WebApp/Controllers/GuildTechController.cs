using EnumsNET;
using Microsoft.AspNetCore.Mvc;
using MoeWiki.WebApp.Helpers;
using MoeWiki.WebApp.Models.GuildTech;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Gateways;
using Newtonsoft.Json.Linq;
using System;

namespace MoeWiki.WebApp.Controllers
{
    public class GuildTechController : Controller
    {
        public IActionResult Index()
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"GT");
            return View(GatewaysFacade.ResearchGateway.GetAllForTechTree());
        }

        [HttpGet]
        public IActionResult GetFilter(int id)
        {
            return View("~/Views/GuildTech/_Filter.cshtml", GatewaysFacade.ResearchGateway.GetForFilterById(id));
        }
        [HttpPost]
        public IActionResult Calculate(GTFilter filter)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"GTCalc");
            var model = new List<TechItem>();
            model.AddRange(GetPrevItem(filter.TargetId, filter.CheckedIds));
            var research = GatewaysFacade.ResearchGateway.GetByIdIncludeItems(filter.TargetId);
            var actReq = CalculatorHelper.GetGuildActRec(research.GuildLevel) - CalculatorHelper.GetGuildActRec(filter.GuildLvlHave);
            model.Add(new TechItem() { Text = $"Guild Level {filter.GuildLvlHave}->{research.GuildLevel}", ActReq = actReq > 0 ? actReq : 0 });
            return View("~/Views/GuildTech/_Result.cshtml", model);
        }

        private List<TechItem> GetPrevItem(int id, List<int> param)
        {
            var result = new List<TechItem>();
            var research = GatewaysFacade.ResearchGateway.GetByIdIncludeItems(id);
            if (!param.Contains(id))
            {
                var item = new TechItem() { Text = research.Name, ActReq = research.GuildActivity, CoinsReq = research.CopperCoins };
                result.Add(item);
            }
            if (research.PreviousId != null && research.PreviousId != 0) { 
                result.AddRange(GetPrevItem(research.PreviousId.Value, param));
            }
            return result;
        }
    }
}
