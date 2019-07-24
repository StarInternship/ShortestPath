using System.IO;
using System.Text.RegularExpressions;

namespace ShortestPath.models
{
    class GraphReader
    {
        private static readonly Regex regex = new Regex(@"^(\d+)\D+(\d+)\D+(\d+)$");

        public Graph Read(string path)
        {
            if (File.Exists(path))
            {
                string[] edges = File.ReadAllLines(path);
                int n = int.Parse(edges[0]);
                Graph graph = new Graph(n);

                for (int i = 1; i < edges.Length; i++)
                {
                    ReadEdge(graph, edges[i]);
                }
                return graph;
            }
            return new Graph(0);
        }

        private static void ReadEdge(Graph graph, string edge)
        {
            var groups = regex.Matches(edge)[0].Groups;

            int source = int.Parse(groups[1].ToString());
            int destination = int.Parse(groups[2].ToString());
            int weight = int.Parse(groups[3].ToString());

            graph.AddEdge(source, destination, weight);
        }
    }
}
