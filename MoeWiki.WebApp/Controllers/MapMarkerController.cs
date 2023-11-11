using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoeWiki.WebApp.Models;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Helpers;
using MOEWiki.DBMySQL.Models;

namespace MoeWiki.WebApp.Controllers
{
    public class MapMarkerController : Controller
    {

        // GET: MapMarkerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new MapMarkerModel(id));
        }

        // POST: MapMarkerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MapMarkerModel model)
        {
            try
            {
                var entity = new MapMarker();
                model.MapTo(entity);
                GatewaysFacade.MapMarkerGateway.Update(entity);
                ViewBag.Result = "Saved";
                return View(model);
            }
            catch
            {
                return View();
            }
        }
    }
}
