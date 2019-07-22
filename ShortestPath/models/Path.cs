using System.Collections.Generic;

namespace ShortestPath.models
{
    public class Path : LinkedList<Edge>
    {
        public double Distance { get; private set; }

        public void Add(Edge edge)
        {
            AddFirst(edge);
            Distance += edge.Weight;
        }
    }
}