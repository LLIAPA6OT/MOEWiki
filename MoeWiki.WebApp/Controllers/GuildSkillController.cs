using Microsoft.AspNetCore.Mvc;
using MoeWiki.WebApp.Models.GuildSkill;
using MoeWiki.WebApp.Helpers;
using MOEWiki.DBMySQL.Gateways;
using System.Globalization;

namespace MoeWiki.WebApp.Controllers
{
    public class GuildSkillController : Controller
    {
        public IActionResult Index(int id = 0)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"GT");
            ViewBag.Id = id;
            return View(GatewaysFacade.GuildSkillGateway.GetAllForView());
        }

        [HttpGet]
        public IActionResult GetFilter(int id)
        {
            return View("~/Views/GuildSkill/_Filter.cshtml", GatewaysFacade.GuildSkillGateway.GetForFilterById(id));
        }
        [HttpPost]
        public IActionResult Calculate(GSFilter filter)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"GSCalc");
            var model = new List<ReqItem>();
            model.AddRange(GetReqItem(filter.TargetId, filter.ReqPairs[filter.TargetId], filter.TargetValue, filter.ReqPairs));
            var req = GatewaysFacade.GuildSkillGateway.GetReq(filter.TargetId, filter.TargetValue);
            var actReq = CalculatorHelper.GetGuildActRec(req.GuildLevel) - CalculatorHelper.GetGuildActRec(filter.GuildLvlHave);
            model.Add(new ReqItem() { Text = "Guild Level", ActReq = actReq > 0 ? actReq : 0, HaveLvl = filter.GuildLvlHave, ReqLvl = req.GuildLevel });
            return View("~/Views/GuildSkill/_Result.cshtml", model);
        }

        private List<ReqItem> GetReqItem(int id, int have, int req, Dictionary<int, int> param)
        {
            var result = new List<ReqItem>();
            var gsr = GatewaysFacade.GuildSkillGateway.GetByIdIncludeReqAndRel(id);
            var item = new ReqItem() { Text = gsr.Name, ReqLvl = req, HaveLvl = have };
            foreach (var v in gsr.GuildSkillRequires.Where(w => w.Level > have && w.Level <= req))
            {
                item.ActReq += v.GuildActivity;
                item.CoinsReq += v.CopperCoins;
            }
            result.Add(item);            

            foreach (var v in gsr.GuildSkillRelations)
            {
                result.AddRange(GetReqItem(v.PrevGuildSkillId, param[v.PrevGuildSkillId], gsr.GuildSkillRequires.First(f => f.Level == req).PrevLevel, param));
            }
            return result;
        }
        
        [HttpPost]
        public JsonResult GetParentValueNeed(int id, int val)
        {
            var req = GatewaysFacade.GuildSkillGateway.GetReq(id, val);
            if (req == null)
                return Json(new { Result = 0 });
            else
                return Json(new { Result = req.PrevLevel });
        }
        [HttpPost]
        public JsonResult EquateToParent(int id, int parentVal)
        {
            var req = GatewaysFacade.GuildSkillGateway.GetAllRequiresByGSId(id).Where(w => w.PrevLevel <= parentVal).OrderByDescending(o => o.Level).FirstOrDefault();
            if (req == null)
                return Json(new { Result = 0 });
            else
                return Json(new { Result = req.Level });
        }
    }
}
