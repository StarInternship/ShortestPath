using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    public class Node
    {
        public string Index { get; }
        public List<Edge> Outs { get; } = new List<Edge>();
        public bool Visited { get; set; }
        public double Distance { get; set; } = Double.MaxValue;
        public List<Edge> LastInEdges { get; set; } = new List<Edge>();

        public Node(string index) => Index = index;

        public void Reset()
        {
            Visited = false;
            Distance = Double.MaxValue;
            LastInEdges = new List<Edge>();
        }

        public void AddEdge(Edge edge)
        {
            Outs.Add(edge);
        }

        public void AddInEdge(Edge edge)
        {
            LastInEdges.Add(edge);
        }

        public void RecreateInEdges(Edge edge, double newDistance)
        {
            LastInEdges.Clear();
            LastInEdges.Add(edge);
            Distance = newDistance;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || !(obj is Node)) return false;
            return Index.Equals(((Node)obj).Index);
        }

        public override string ToString() => Index;
    }
}
