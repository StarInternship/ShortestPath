using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShortestPath.models
{
    class GraphReader
    {
        public Graph Read(string path)
        {
            if (File.Exists(path))
            {
                string[] edges = File.ReadAllLines(path);
                int n = int.Parse(edges[0]);
                Graph graph = new Graph(n);
                for (int i = 1; i < edges.Length; i++)
                {
                    string[] edgeString = Regex.Split(edges[i], ",");
                    int src = int.Parse(edgeString[0]);
                    int dest = int.Parse(edgeString[1]);
                    int weight = int.Parse(edgeString[2]);
                    graph.AddEdge(src, dest, weight);
                }
                return graph;
            }
            return new Graph(0);
        }
    }
}
