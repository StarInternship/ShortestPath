

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
        public JsonResult Search(string query)
        {
            return null;
        }

        [HttpPost]
        public JsonResult GetGraphsList() => Json(MainController.Instance.GetGraphsList());

        [HttpPost]
        public JsonResult ImportGraph(string graphName) => Json(MainController.Instance.ImportGraph(graphName));
    }
}