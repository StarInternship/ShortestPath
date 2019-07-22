using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    public class Node
    {
        public int Index { get; }
        public LinkedList<Edge> Outs { get; } = new LinkedList<Edge>();
        public bool Visited { get; set; }
        public double Distance { get; set; } = Double.MaxValue;

        public Node(int index) => Index = index;
    }
}
