

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

        [HttpGet]
        public JsonResult GetGraphsList() =>Json(MainController.Instance.GetGraphsList());
    }
}