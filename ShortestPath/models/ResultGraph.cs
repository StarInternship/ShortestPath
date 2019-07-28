using System;
using System.Collections.Generic;

namespace ShortestPath.models
{
    /// <summary>
    /// search result subgraph
    /// </summary>
    public class ResultGraph : Graph
    {
        /// <summary>
        /// set of all edges of result subgraph
        /// </summary>
        public HashSet<Edge> AllEdges { get; } = new HashSet<Edge>();

        /// <summary>
        /// adds an edge to graph
        /// </summary>
        /// <param name="from">edge starting node index, if does'n exists, it will be created</param>
        /// <param name="to">edge ending node index, if does'n exists, it will be created</param>
        /// <param name="weight">weight of edge</param>
        public new void AddEdge(string from, string to, double weight) => AddEdge(GetNode(from), GetNode(to), weight);

        /// <summary>
        /// adds an edge to graph
        /// </summary>
        /// <param name="from">edge starting node, if does'n exists, it will be created</param>
        /// <param name="to">edge ending node, if does'n exists, it will be created</param>
        /// <param name="weight">weight of edge</param>
        public new void AddEdge(Node from, Node to, double weight) => AllEdges.Add(base.AddEdge(from, to, weight));

        /// <summary>
        /// returns true if the node of graph is exploring by path finder
        /// </summary>
        /// <param name="index"></param>
        /// <returns>true if the node of graph is exploring by path finder</returns>
        public bool Exploring(string index)
        {
            if (!Nodes.ContainsKey(index)) return false;
            return Nodes[index].Exploring;
        }
    }
}
