using EnumsNET;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using MoeWiki.WebApp.Helpers;
using MoeWiki.WebApp.Models.Home;
using MoeWiki.WebApp.Models.Item;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Models;
using NuGet.Packaging;
using System.Reflection.Metadata;
using System.Reflection;
using System;

namespace MoeWiki.WebApp.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Items";
            return View(GatewaysFacade.CategoryGateway.GetAllIncludeSubcat());
        }
        public IActionResult TopWeapon()
        {
            ViewData["Title"] = "TopWeapon";
            var model = new List<SubCat>() { 
                new SubCat() {Id = 2, Name = "One-handed", ActiveTab = "Meele"},
                new SubCat() {Id = 12, Name = "Polearm", ActiveTab = "Meele"},
                new SubCat() {Id = 13, Name = "Two-handed", ActiveTab = "Meele"},
                new SubCat() {Id = 33, Name = "Shield", ActiveTab = "Meele"},
                new SubCat() {Id = 36, Name = "Bow", ActiveTab = "Range"},
                new SubCat() {Id = 35, Name = "Crossbow", ActiveTab = "Range"},
                new SubCat() {Id = 26, Name = "Arrow", ActiveTab = "Ammo"},
                new SubCat() {Id = 54, Name = "Heavy Bolt", ActiveTab = "Ammo"},
                new SubCat() {Id = 55, Name = "Light Bolt", ActiveTab = "Ammo"},
                new SubCat() {Id = 10, Name = "Light Armor", ActiveTab = "Armor"},
                new SubCat() {Id = 28, Name = "Heavy Armor", ActiveTab = "Armor"},
                new SubCat() {Id = 27, Name = "Mount Armor", ActiveTab = "Armor"}
            };
            return View(model);
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
        

        [HttpGet]
        public IActionResult GetTopWeapon(string s1, string s2, string s3, string s4, string s5, string s6)
        {
            var filter = new TopWeapon() 
            { 
                SubcatsIdsSelected = s1.Split('|').Where(w => !string.IsNullOrWhiteSpace(w)).Select(s => int.Parse(s)).ToArray(), 
                QualitiesSelected = s3.Split('|').Where(w => !string.IsNullOrWhiteSpace(w)).ToArray(), 
                LevelsSelected = s2.Split('|').Where(w => !string.IsNullOrWhiteSpace(w)).ToArray(), 
                ActiveTab = s4, 
                OrderBy = s5, 
                Desc = bool.Parse(s6) };
            var list = GatewaysFacade.ItemGateway.GetByTopWeaponFilter(filter.SubcatsIdsSelected, filter.LevelsSelected);
            if (string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                switch (filter.ActiveTab)
                {
                    case "Meele":
                        filter.OrderBy = "Name";
                        break;
                    case "Range":
                        filter.OrderBy = "Name";
                        break;
                    case "Ammo":
                        filter.OrderBy = "Name";
                        break;
                    case "Armor":
                        filter.OrderBy = "Name";
                        break;
                    default:
                        break;
                }
            }
            filter.OrderBy = filter.OrderBy.Replace('_', ' ');
            switch (filter.OrderBy)
            {
                case "Name":
                    if (filter.Desc)
                        list = list.OrderByDescending(o => o.Name);
                    else
                        list = list.OrderBy(o => o.Name);
                    break;
                case "Type":
                    if (filter.Desc)
                        list = list.OrderByDescending(o => o.Subcategory.Name);
                    else
                        list = list.OrderBy(o => o.Subcategory.Name);
                    break;
                default:
                    var props = list.SelectMany(s => s.ItemProperties).Where(a => a.Name == filter.OrderBy);
                    if (props.Any())
                    {
                        switch (props.First().Property.PropertyType)
                        {
                            case MOEWiki.DBMySQL.Enums.PropertyTypeEnum.Text:
                                if (filter.Desc)
                                    list = list.OrderByDescending(o => o.ItemProperties.FirstOrDefault(f => f.Name == filter.OrderBy && f.Quality == MOEWiki.DBMySQL.Enums.QualityEnum.Low)?.Value ?? "");
                                else
                                    list = list.OrderBy(o => o.ItemProperties.FirstOrDefault(f => f.Name == filter.OrderBy && f.Quality == MOEWiki.DBMySQL.Enums.QualityEnum.Low)?.Value ?? "");
                                break;
                            case MOEWiki.DBMySQL.Enums.PropertyTypeEnum.Number:
                                if (filter.Desc)
                                    list = list.OrderByDescending(o => Convert.ToDouble(o.ItemProperties.FirstOrDefault(f => f.Name == filter.OrderBy && f.Quality == MOEWiki.DBMySQL.Enums.QualityEnum.Low)?.Value.Trim() ?? "0"));
                                else
                                    list = list.OrderBy(o => Convert.ToDouble(o.ItemProperties.FirstOrDefault(f => f.Name == filter.OrderBy && f.Quality == MOEWiki.DBMySQL.Enums.QualityEnum.Low)?.Value.Trim() ?? "9999999999"));
                                break;
                            case MOEWiki.DBMySQL.Enums.PropertyTypeEnum.Percent:
                                if (filter.Desc)
                                    list = list.OrderByDescending(o => Convert.ToDouble(o.ItemProperties.FirstOrDefault(f => f.Name == filter.OrderBy && f.Quality == MOEWiki.DBMySQL.Enums.QualityEnum.Low)?.Value.Replace("%","").Trim() ?? "0"));
                                else
                                    list = list.OrderBy(o => Convert.ToDouble(o.ItemProperties.FirstOrDefault(f => f.Name == filter.OrderBy && f.Quality == MOEWiki.DBMySQL.Enums.QualityEnum.Low)?.Value.Replace("%", "").Trim() ?? "9999999999"));
                                break;
                        }
                    }
                    break;
            }
            filter.Items = list.ToList();
            filter.OrderBy = filter.OrderBy.Replace(' ', '_');
            return View("~/Views/Item/_Top"+filter.ActiveTab+".cshtml", filter);
        }


    }
}
