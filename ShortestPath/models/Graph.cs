using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestPath.models
{
    public class Graph
    {
        public Dictionary<string, Node> Nodes { get; } = new Dictionary<string, Node>();

        /// <summary>
        /// create node if does not exist. and returns the node.
        /// </summary>
        /// <param name="index">index of the node</param>
        /// <returns>Node</returns>
        public Node GetNode(string index)
        {
            if (!Nodes.ContainsKey(index))
            {
                Nodes[index] = new Node(index);
            }
            return Nodes[index];
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
        /// <param name="dest">destindation</param>
        /// <param name="findAllPath">if true method returns all paths from source to destination</param>
        /// <returns>expected subgraph</returns>
        public Graph FindPaths(string src, string dest, bool findAllPath) => FindPaths(GetNode(src), GetNode(dest), findAllPath);

        private void AddEdge(Node from, Node to, double weight)
        {
            Edge edge = new Edge(from, to, weight);
            from.AddOut(edge);
            to.AddIn(edge);
        }
        private Graph FindPaths(Node source, Node destination, bool findAllPath)
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
                    edge => UpdateEdgeDestination(destination, currentNodes, node, edge, findAllPath)
                );
            }
            return CreateResultSubgraph(destination);
        }

        private void UpdateEdgeDestination(Node target, PriorityQueue currentNodes, Node node, Edge edge, bool findAllPath)
        {
            if (findAllPath)
            {
                edge.To.AddLastInEdge(edge);
                if (!edge.To.Visited)
                {
                    currentNodes.Add(edge.To);
                }
                if (IsAShorterPath(node, edge))
                {
                    edge.To.Distance = node.Distance + edge.Weight;
                }
            }
            else if (PosiblePath(target, node, edge))
            {
                if (IsAShorterPath(node, edge))
                {
                    edge.To.RecreateInEdges(edge, node.Distance + edge.Weight);
                    currentNodes.Add(edge.To);
                }
                else if (IsEqualPath(node, edge))
                {
                    edge.To.AddLastInEdge(edge);
                }
            }
        }

        // this path is a shorter path to edge.to?
        private bool IsAShorterPath(Node node, Edge edge) => node.Distance + edge.Weight < edge.To.Distance;

        private bool IsEqualPath(Node node, Edge edge) => node.Distance + edge.Weight == edge.To.Distance;

        // if we already found a shorter path to target, it returns false
        private static bool PosiblePath(Node target, Node node, Edge edge) => target.Distance >= node.Distance + edge.Weight;

        private void Reset()
        {
            foreach (Node node in Nodes.Values)
            {
                node.Reset();
            }
        }

        private Graph CreateResultSubgraph(Node target)
        {
            var subgraph = new Graph();
            var currentNodes = new HashSet<Node> { target };

            while (currentNodes.Count > 0)
            {
                var node = currentNodes.First();
                currentNodes.Remove(node);

                if (subgraph.Visited(node.Index)) continue;
                subgraph.GetNode(node.Index).Visited = true;

                node.LastInEdges.ForEach(edge =>
                { 
                    subgraph.AddEdge(edge.From.Index, edge.To.Index, edge.Weight);
                    currentNodes.Add(edge.From);
                });
            }

            return subgraph;
        }

        private bool Visited(string index)
        {
            if (!Nodes.ContainsKey(index)) return false;
            return Nodes[index].Visited;
        }
    }
}
