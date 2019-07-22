using System.Collections.Generic;

namespace ShortestPath.models
{
    public class Path : LinkedList<Edge>
    {
        public double Distance { get; private set; }

        public void Add(Edge edge) => AddLast(edge);

        public new void AddFirst(Edge edge)
        {
            base.AddFirst(edge);
            Distance += edge.Weight;
        }

        public new void AddLast(Edge edge)
        {
            base.AddLast(edge);
            Distance += edge.Weight;
        }
    }
}