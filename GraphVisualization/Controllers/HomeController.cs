

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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult Search(string query)
        {
            return null;
        }

        [HttpGet]
        public JsonResult GetGraphsList()
        {
            return Json(MainController.Instance.GetGraphsList());
        }



    }
}