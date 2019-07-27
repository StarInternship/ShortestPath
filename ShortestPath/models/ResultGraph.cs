using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    public class ResultGraph : Graph
    {
        public Node Source { get; }
        public Node Destination { get; }
        public HashSet<Edge> AllEdges { get; } = new HashSet<Edge>();

        public void AddEdge(string from, string to, double weight) => AddEdge(GetNode(from), GetNode(to), weight);

        public void AddEdge(Node from, Node to, double weight) => AllEdges.Add(base.AddEdge(from, to, weight));
    }
}
