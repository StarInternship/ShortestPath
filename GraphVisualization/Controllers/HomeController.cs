﻿

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

        [HttpPost]
        public JsonResult Search(string source, string destination, int maxDistance, bool findAllPaths, bool directed) => 
            Json(MainController.Instance.Search(source, destination, maxDistance, findAllPaths, directed));
    }
}