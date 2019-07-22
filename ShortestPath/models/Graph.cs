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

        public void AddEdge(int from, int to, double weight) => GetNode(from).Outs.AddLast(new Edge(from, to, weight));

        public Path FindePath(Node source, Node target)
        {
            source.Distance = 0;

            PriorityQueue currentNodes = new PriorityQueue();

            currentNodes.Add(source);

            while (!currentNodes.IsEmpty())
            {
                Node node = currentNodes.Pop();

                if (node.Visited) continue;
                node.Visited = true;

                foreach (Edge edge in node.Outs)
                {
                    UpdateEdgeDestination(target, currentNodes, node, edge);
                }
            }
            Path path = new Path();

            Edge lastEdge = target.LastInEdge;
            while (GetNode(lastEdge.From).Index != source.Index)
            {
                path.Add(lastEdge);
                lastEdge = GetNode(lastEdge.From).LastInEdge;
            }
            return path;
        }

        private void UpdateEdgeDestination(Node target, PriorityQueue currentNodes, Node node, Edge edge)
        {
            if (PosiblePath(target, node, edge) && IsAShorterPath(node, edge))
            {
                currentNodes.Add(GetNode(edge.To));
                GetNode(edge.To).Distance = node.Distance + edge.Weight;
                GetNode(edge.To).LastInEdge = edge;
            }
        }

        // this path is a shorter path to edge.to?
        private bool IsAShorterPath(Node node, Edge edge)
        {
            return node.Distance + edge.Weight < GetNode(edge.To).Distance;
            
        }

        // if we already found a shorter path to target, it returns false
        private static bool PosiblePath(Node target, Node node, Edge edge)
        {
            return target.Distance > node.Distance + edge.Weight;
        }
    }
}
