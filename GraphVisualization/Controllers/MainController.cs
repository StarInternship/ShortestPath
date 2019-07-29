using GraphVisualization.Models;
using ShortestPath.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace GraphVisualization.Controllers
{
    public class MainController
    {
        public static MainController Instance { get; } = new MainController();
        private string graphPath = HttpContext.Current.Server.MapPath("~/TestFiles");
        private MainController()
        {
        }

        public GraphsList GetGraphsList()
        {
            var list = new GraphsList();

            foreach (string path in Directory.GetFiles(graphPath))
            {
                list.List.Add(Path.GetFileName(path));
            }
            return list;
        }

        public GraphContainer ImportGraph(string graphName)
        {
            ResultGraph graph = new GraphReader().ReadGraphResult(graphPath + "/" + graphName);
            return new GraphContainer(graph);

        }
    }
}