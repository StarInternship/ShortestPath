using ShortestPath.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphVisualization.Models
{
    public class GraphContainer
    {
        public List<EdgeContainer> edges { get; } = new List<EdgeContainer>();
        public List<NodeContainer> nodes { get; } = new List<NodeContainer>();


        public GraphContainer(Graph graph)
        {
            Random random = new Random();
            foreach (Node node in graph.Nodes.Values)
            {
                nodes.Add(new NodeContainer(node.Index, random.Next() % 400, random.Next() % 400));
            }

            foreach (Edge edge in graph.AllEdges)
            {
                edges.Add(new EdgeContainer(edge));
            }
        }
    }
}