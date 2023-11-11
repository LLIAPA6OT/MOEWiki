using Microsoft.AspNetCore.Mvc;
using MOEWiki.DBMySQL.Gateways;

namespace MoeWiki.WebApp.Controllers
{
    public class PerkController : Controller
    {
        public IActionResult Index(int id = 0)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"perk");
            ViewBag.Id = id;
            return View(GatewaysFacade.PerkGateway.GetAll());
        }
    }
}
