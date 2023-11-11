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
    public class FieldController : Controller
    {
        public IActionResult Index()
        {
            GatewaysFacade.WikiActionGateway.GenerateThenAdd(HttpContext.Connection.RemoteIpAddress?.ToString(), $"field");
            return View();
        }

    }
}
