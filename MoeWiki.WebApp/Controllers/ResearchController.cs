using Microsoft.AspNetCore.Mvc;
using MOEWiki.DBMySQL.Gateways;

namespace MoeWiki.WebApp.Controllers
{
    public class ResearchController : Controller
    {
        public IActionResult Index()
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"research");
            return View(GatewaysFacade.ResearchGateway.GetAllIncludeItems());
        }
    }
}
