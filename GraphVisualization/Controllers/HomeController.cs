

using System.Collections.Generic;
using System.Web.Mvc;

namespace GraphVisualization.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetGraphsList() => Json(MainController.Instance.GetGraphsList());

        [HttpPost]
        public JsonResult ImportGraph(string graphName)
        {
            JsonResult json = Json(MainController.Instance.ImportGraph(graphName));
            return json;
        }
    }
}