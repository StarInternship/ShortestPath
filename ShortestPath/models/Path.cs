using System.Collections.Generic;
using System.Runtime.Serialization;

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