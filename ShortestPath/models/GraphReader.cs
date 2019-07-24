using System.IO;
using System.Text.RegularExpressions;

namespace ShortestPath.models
{
    class GraphReader
    {
        private static readonly Regex regex = new Regex(@"^(\d+)\D+(\d+)\D+(\d+.?\d*)$");

        public Graph Read(string path)
        {
            if (File.Exists(path))
            {
                string[] edges = File.ReadAllLines(path);
                Graph graph = new Graph();

                for (int i = 1; i < edges.Length; i++)
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
    }
}
