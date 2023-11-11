using Microsoft.AspNetCore.Mvc;
using MOEWiki.DBMySQL.Gateways;

namespace MoeWiki.WebApp.Controllers
{
    public class StaticPageController : Controller
    {
        public IActionResult Page(int id)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"static {id}");
            return View($"/Views/Static/{id}.cshtml");
        }
    }
}
