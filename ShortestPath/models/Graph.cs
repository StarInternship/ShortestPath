using System.Collections.Generic;
using System.Linq;

namespace ShortestPath.models
{
    public class Graph
    {
        private readonly Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        /// <summary>
        /// create node if does not exist. and returns the node.
        /// </summary>
        /// <param name="index">index of the node</param>
        /// <returns>Node</returns>
        public Node GetNode(string index)
        {
            if (!nodes.ContainsKey(index))
            {
                nodes[index] = new Node(index);
            }
            return nodes[index];
        }

        /// <summary>
        /// add an edge to graph
        /// </summary>
        /// <param name="from">edge strting node index, if does'n exists, it will be created</param>
        /// <param name="to">edge ending node index, if does'n exists, it will be created</param>
        /// <param name="weight">wheight of edge</param>
        public void AddEdge(string from, string to, double weight) => AddEdge(GetNode(from), GetNode(to), weight);

        /// <summary>
        /// finds shortest paths between src and dest
        /// </summary>
        /// <param name="src">source</param>
        /// <param name="dest">destination</param>
        /// <returns>all shortest paths</returns>
        public List<Path> FindShortestPaths(string src, string dest) => FindShortestPaths(GetNode(src), GetNode(dest));

        private void AddEdge(Node from, Node to, double weight) => from.AddEgde(new Edge(from, to, weight));

        private List<Path> FindShortestPaths(Node source, Node destination)
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

                node.Outs.ForEach(
                    edge => UpdateEdgeDestination(destination, currentNodes, node, edge)
                );
            }
            return CreatePaths(source, destination);
        }

        private void Reset()
        {
            foreach (Node node in nodes.Values)
            {
                node.Reset();
            }
        }

        private List<Path> CreatePaths(Node source, Node target)
        {
            var result = new List<Path>();
            var currentState = new LinkedList<Path>();

            target.LastInEdges.ForEach(edge => currentState.AddLast(new Path(target) { edge }));

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
                    ExpandPath(currentState, path, firstNode);
                }
            }
            return result;
        }

        private static void ExpandPath(LinkedList<Path> currentState, Path path, Node firstNode)
        {
            firstNode.LastInEdges.ForEach(edge =>
            {
                if (path.ContainsNode(edge.From)) return;
                Path newPath = new Path(path);
                newPath.AddFirst(edge);
                currentState.AddLast(newPath);
            });
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
        private bool IsAShorterPath(Node node, Edge edge) => node.Distance + edge.Weight < edge.To.Distance;

        private bool IsEqualPath(Node node, Edge edge) => node.Distance + edge.Weight == edge.To.Distance;

        // if we already found a shorter path to target, it returns false
        private static bool PosiblePath(Node target, Node node, Edge edge) => target.Distance >= node.Distance + edge.Weight;
    }
}
