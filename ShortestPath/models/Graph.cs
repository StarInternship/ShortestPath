using System.Collections.Generic;

namespace ShortestPath.models
{
    class Graph
    {
        private readonly Node[] nodes;

        public Graph(int n)
        {
            nodes = new Node[n];
            for (int i = 0; i < n; i++)
            {
                nodes[i] = new Node(i);
            }
        }

        public Node GetNode(int index) => nodes[index];

        public Path FindePath(Node source, Node target)
        {
            source.Distance = 0;

            PriorityQueue nodes = new PriorityQueue();

            nodes.Add(source);

            while (!nodes.IsEmpty())
            {
                Node node = nodes.Pop();

                if (node.Visited) continue;
                node.Visited = true;

                foreach (Edge edge in node.Outs)
                {
                    if (target.Distance > node.Distance + edge.Weight && node.Distance + edge.Weight < edge.To.Distance)
                    {
                        nodes.Add(edge.To);
                        edge.To.Distance = node.Distance + edge.Weight;
                        edge.To.LastInEdge = edge;
                    }
                }
            }
            Path path = new Path();

            Edge lastEdge = target.LastInEdge;
            while (lastEdge.From.Index != source.Index)
            {
                path.Add(lastEdge);
                lastEdge = lastEdge.From.LastInEdge;
            }
            return path;
        }
    }
}
