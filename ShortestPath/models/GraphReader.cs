using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ShortestPath.models
{
   public class GraphReader
    {
        private static readonly Regex regex = new Regex(@"^(.+),(.+),(\d+.?\d*)$");

        public Graph ReadGraph(string path)
        {
            if (File.Exists(path))
            {
                string[] edges = File.ReadAllLines(path);
                Graph graph = new Graph();

                for (int i = 0; i < edges.Length; i++)
                {
                    ReadEdge(graph, edges[i]);
                }
                return graph;
            }
            return new Graph();
        }

        private static void ReadEdge(Graph graph, string edge)
        {
            var groups = regex.Matches(edge)[0].Groups;

            string source = groups[1].ToString();
            string destination = groups[2].ToString();
            double weight = int.Parse(groups[3].ToString());

            graph.AddEdge(source, destination, weight);
        }

        public ResultGraph ReadGraphResult(string path)
        {
            if (File.Exists(path))
            {
                string[] edges = File.ReadAllLines(path);
                var graph = new ResultGraph();

                for (int i = 0; i < edges.Length; i++)
                {
                    var groups = regex.Matches(edges[i])[0].Groups;

                    string source = groups[1].ToString();
                    string destination = groups[2].ToString();
                    double weight = int.Parse(groups[3].ToString());

                    graph.AddEdge(source, destination, weight);
                }
                return graph;
            }
            return new ResultGraph();
        }
    }
}
