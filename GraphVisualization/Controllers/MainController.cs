﻿using GraphVisualization.Models;
using ShortestPath.models;
using System.IO;
using System.Web;

namespace GraphVisualization.Controllers
{
    public class MainController
    {
        public static MainController Instance { get; } = new MainController();
        private string graphPath = HttpContext.Current.Server.MapPath("~/TestFiles");
        private ResultGraph currentGraph;
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

        public GraphContainer Search(string source, string destination, int maxDistance, bool findAllPaths) =>
            new GraphContainer(
                new PathFinder(currentGraph, source, destination, findAllPaths, maxDistance).Find()
            );

        public GraphContainer ImportGraph(string graphName)
        {
            currentGraph = new GraphReader().ReadGraphResult(graphPath + "/" + graphName);
            return new GraphContainer(currentGraph);
        }
    }
}