using System;
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

        public void AddEdge(int from, int to, double weight) => GetNode(from).AddEgde(new Edge(from, to, weight));

        public Path FindPath(int src,int dest)
        {
            return FindPath(GetNode(src), GetNode(dest));
        }

        public Path FindPath(Node source, Node target)
        {
            Reset();
            source.Distance = 0;
            PriorityQueue currentNodes = new PriorityQueue();
            currentNodes.Add(source);

            while (!currentNodes.IsEmpty())
            {
                Node node = currentNodes.Pop();

                if (node.Visited) continue;
                node.Visited = true;

                node.Outs.ForEach(edge => UpdateEdgeDestination(target, currentNodes, node, edge));
            }
            return CreatePath(source, target);
        }

        private void Reset()
        {
            foreach (Node node in nodes)
            {
                node.Reset();
            }
        }

        private Path CreatePath(Node source, Node target)
        {
            Path path = new Path();

            Edge lastEdge = target.LastInEdge;
            if (lastEdge == null) return Path.NOT_FOUND;
            AddEdgesToPath(source, path, lastEdge);
            return path;
        }

        private void AddEdgesToPath(Node source, Path path, Edge lastEdge)
        {
            while (lastEdge.From != source.Index)
            {
                path.Add(lastEdge);
                lastEdge = GetNode(lastEdge.From).LastInEdge;
            }
            path.Add(lastEdge);
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
