using EnumsNET;
using Microsoft.AspNetCore.Mvc;
using MoeWiki.WebApp.Helpers;
using MoeWiki.WebApp.Models.Item;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Models;
using NuGet.Packaging;

namespace MoeWiki.WebApp.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Items";
            return View(GatewaysFacade.CategoryGateway.GetAllIncludeSubcat());
        }
        public IActionResult HomeIndex()
        {
            ViewData["Title"] = "MOE Wiki";
            return View("/Views/Item/Index.cshtml", GatewaysFacade.CategoryGateway.GetAllIncludeSubcat());
        }
        public IActionResult List(int subcategoryId)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"Subcategory{subcategoryId}");
            var subcat = GatewaysFacade.SubcategoryGateway.GetById(subcategoryId);
            if (subcat != null)
            {
                ViewData["Title"] = subcat.Name;
            }
            return View(GatewaysFacade.ItemGateway.GetAllBySubcategoryId(subcategoryId));
        }
        public IActionResult Details(int id)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"Details{id}");
            var v = GatewaysFacade.ItemGateway.GetByIdForDetails(id);
            return View(v);
        }
        [HttpGet]
        public IActionResult GetPrimitive(int id)
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"GetPrimitive{id}");
            var entity = GatewaysFacade.ItemGateway.GetById(id);
            var raw = CalculatorHelper.GetPrimitive(new List<RecipeItem>() { new RecipeItem() { Id = entity.Id, Name = entity.Name, Count = 1, Number = 1 } } );
            var model = new List<RecipeItem>();
            foreach ( var group in raw.Where(w => !w.IsDeleted).GroupBy(g => g.Id))
            {
                model.Add(new RecipeItem() { Id = group.Key, Name = group.First().Name, Number = group.First().Number, IsStepByStep = group.First().IsStepByStep, Count = group.Sum(s => s.Count)});
            }
            return View("~/Views/Item/_Primitive.cshtml", model);
        }


        [HttpGet]
        public IActionResult GetFullSequence(int id)
        {
            return View("~/Views/GuildSkill/_Filter.cshtml", GatewaysFacade.GuildSkillGateway.GetForFilterById(id));
        }


    }
}
