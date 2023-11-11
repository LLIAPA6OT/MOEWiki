using EnumsNET;
using Microsoft.AspNetCore.Mvc;
using MoeWiki.WebApp.Models.Map;
using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Gateways;

namespace MoeWiki.WebApp.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index(int id = 1)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"Map{id}");
            var model = new MapModel(GatewaysFacade.MapGateway.GetById(id));
            return View(model);
        }

        public IActionResult MapZone(int id)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"MapZone{id}");
            var model = new MapModel(GatewaysFacade.MapZoneGateway.GetById(id));
            return View("\\Views\\Map\\Index.cshtml", model);
        }

        public IActionResult MarkerGroup(int id)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"MarkerGroup{id}");
            var model = new MapModel(GatewaysFacade.MarkerGroupGateway.GetById(id));
            return View("\\Views\\Map\\Index.cshtml", model);
        }
    }
}
