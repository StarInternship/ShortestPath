using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    public class Path : LinkedList<Edge>
    {
        private readonly HashSet<Node> nodeSet;
        public double Distance { get; private set; }

        public Path() : base() => nodeSet = new HashSet<Node>();

        public Path(Path path) : base(path)
        {
            Distance = path.Distance;
            nodeSet = new HashSet<Node>(path.nodeSet);
        }

        public Path(Node initialNode) : base() => nodeSet = new HashSet<Node>() { initialNode };

        public void Add(Edge edge) => AddLast(edge);

        public new void AddFirst(Edge edge)
        {
            base.AddFirst(edge);
            nodeSet.Add(edge.From);
            Distance += edge.Weight;
        }

        public new void AddLast(Edge edge)
        {
            base.AddLast(edge);
            nodeSet.Add(edge.To);
            Distance += edge.Weight;
        }

        public bool ContainsNode(Node node) => nodeSet.Contains(node);
    }
}