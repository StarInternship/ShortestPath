using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    public class Node
    {
        public int Index { get; }
        public List<Edge> Outs { get; } = new List<Edge>();
        public bool Visited { get; set; }
        public double Distance { get; set; } = Double.MaxValue;
        public Edge LastInEdge { get; set; }

        public Node(int index) => Index = index;

        public void Reset()
        {
            Visited = false;
            Distance = Double.MaxValue;
            LastInEdge = null;
        }

        public void AddEgde(Edge edge)
        {
            Outs.Add(edge);
        }
    }
}
