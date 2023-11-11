using Microsoft.AspNetCore.Mvc;
using MOEWiki.DBMySQL.Gateways;

namespace MoeWiki.WebApp.Controllers
{
    public class GuideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
