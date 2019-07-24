using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    public class Path : Stack<Edge>
    {
        public double Distance { get; private set; }

        public Path() : base() { }

        public Path(Path path) : base(path) => Distance = path.Distance;

        public Path(Node initialNode) : base() { }

        public void Add(Edge edge) => AddFirst(edge);

        public new void AddFirst(Edge edge)
        {
        }

        public bool ContainsNode(Node node) => true;

        public new void Push(Edge edge)
        {
            base.Push(edge);
            Distance += edge.Weight;
        }
    }
}