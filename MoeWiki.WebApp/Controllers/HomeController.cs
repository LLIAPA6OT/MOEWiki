using Microsoft.AspNetCore.Mvc;
using MoeWiki.WebApp.Models;
using MoeWiki.WebApp.Models.Home;
using MOEWiki.DBMySQL.Gateways;
using System.Diagnostics;

namespace MoeWiki.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), "home");
            return RedirectToAction("HomeIndex", "Item");
        }
        public IActionResult SearchResult(string searchString)
        {
            ViewBag.SearchString = searchString;
            var model = new SearchResultModel();
            model.items = GatewaysFacade.ItemGateway.GetAll().Where(w => w.Name.ToLower().Contains(searchString.ToLower())).ToList();
            model.perks = GatewaysFacade.PerkGateway.GetAll().Where(w => w.Name.ToLower().Contains(searchString.ToLower())).ToList();
            model.guildSkills = GatewaysFacade.GuildSkillGateway.GetAll().Where(w => w.Name.ToLower().Contains(searchString.ToLower())).ToList();
            model.subcategories = GatewaysFacade.SubcategoryGateway.GetAll().Where(w => w.Name.ToLower().Contains(searchString.ToLower())).ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
