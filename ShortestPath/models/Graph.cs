using System.Collections.Generic;
using System.Linq;

namespace ShortestPath.models
{
    public class Graph
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

        public void AddEdge(int from, int to, double weight) => GetNode(from).AddEgde(new Edge(GetNode(from), GetNode(to), weight));

        public List<Path> FindAllPaths(int src, int dest)
        {
            return FindAllPaths(GetNode(src), GetNode(dest));
        }

        public List<Path> FindAllPaths(Node source, Node target)
        {
            Reset();
            source.Distance = 0;
            PriorityQueue currentNodes = new PriorityQueue();
            currentNodes.Add(source);

            while (!currentNodes.IsEmpty())
            {
                Node node = currentNodes.Pop();

                if (node.Visited)
                    continue;

                node.Visited = true;

                node.Outs.ForEach(edge => {
                    edge.To.AddInEdge(edge);
                    currentNodes.Add(edge.To);
                });
            }
            return CreatePaths(source, target);
        }


        public List<Path> FindShortestPaths(int src, int dest)
        {
            return FindPath(GetNode(src), GetNode(dest));
        }

        public List<Path> FindPath(Node source, Node target)
        {
            Reset();
            source.Distance = 0;
            PriorityQueue currentNodes = new PriorityQueue();
            currentNodes.Add(source);

            while (!currentNodes.IsEmpty())
            {
                Node node = currentNodes.Pop();

                if (node.Visited)
                    continue;
                node.Visited = true;

                node.Outs.ForEach(edge => UpdateEdgeDestination(target, currentNodes, node, edge));
            }
            return CreatePaths(source, target);
        }

        private void Reset()
        {
            foreach (Node node in nodes)
            {
                node.Reset();
            }
        }

        private List<Path> CreatePaths(Node source, Node target)
        {
            var result = new List<Path>();
            var currentState = new LinkedList<Path>();

            target.LastInEdges.ForEach(edge => currentState.AddLast(new Path() { edge }));

            while (currentState.Count > 0)
            {
                Path path = currentState.First();
                currentState.RemoveFirst();

                Edge firstEdge = path.First();
                Node firstNode = firstEdge.From;
                if (firstNode.Equals(source))
                {
                    result.Add(path);
                }
                else
                {
                    firstNode.LastInEdges.ForEach(edge =>
                    {
                        Path newPath = new Path(path);
                        newPath.AddFirst(edge);
                        currentState.AddLast(newPath);
                    });
                }
            }

            return result;
        }

        private void UpdateEdgeDestination(Node target, PriorityQueue currentNodes, Node node, Edge edge)
        {
            if (PosiblePath(target, node, edge))
            {
                if (IsAShorterPath(node, edge))
                {
                    edge.To.RecreateInEdges(edge, node.Distance + edge.Weight);
                    currentNodes.Add(edge.To);
                }
                else if (IsEqualPath(node, edge))
                {
                    edge.To.AddInEdge(edge);
                }
            }
        }

        // this path is a shorter path to edge.to?
        private bool IsAShorterPath(Node node, Edge edge)
        {
            return node.Distance + edge.Weight < edge.To.Distance;

        }
        private bool IsEqualPath(Node node, Edge edge)
        {
            return node.Distance + edge.Weight == edge.To.Distance;
        }

        // if we already found a shorter path to target, it returns false
        private static bool PosiblePath(Node target, Node node, Edge edge)
        {
            return target.Distance >= node.Distance + edge.Weight;
        }
    }
}
