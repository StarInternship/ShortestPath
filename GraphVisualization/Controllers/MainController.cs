using GraphVisualization.Models;
using ShortestPath.models;
using System.IO;
using System.Web;

namespace GraphVisualization.Controllers
{
    public class MainController
    {
        public static MainController Instance { get; } = new MainController();
        private readonly string graphPath = HttpContext.Current.Server.MapPath("~/TestFiles");
        private Graph currentGraph;
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

        public GraphContainer Search(string source, string destination, int maxDistance, bool findAllPaths, bool directed) =>
            new GraphContainer(
                new PathFinder(currentGraph, source, destination, findAllPaths, maxDistance, directed).Find()
            );

        public GraphContainer ImportGraph(string graphName)
        {
            currentGraph = new GraphReader().ReadGraph(graphPath + "/" + graphName);
            return new GraphContainer(currentGraph);
        }
    }
}