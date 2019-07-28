using System.Collections.Generic;

namespace ShortestPath.models
{

    /// <summary>
    /// A directed and weighted graph model for analyzing
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// A dictionary of each node Index pointing to node
        /// </summary>
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
        public Edge AddEdge(string from, string to, double weight) => AddEdge(GetNode(from), GetNode(to), weight);

        /// <summary>
        /// finds shortest paths between src and dest
        /// </summary>
        /// <param name="src">source</param>
        /// <param name="dest">destindation</param>
        /// <param name="findAllPath">if true method returns all paths from source to destination</param>
        /// <returns>expected subgraph</returns>
        public Edge AddEdge(Node from, Node to, double weight)
        {
            Edge edge = new Edge(from, to, weight);
            from.AddOut(edge);
            return edge;
        }

        /// <summary>
        /// For searching again and again, some parts of graph should be reset.
        /// </summary>
        public void Reset()
        {
            foreach (Node node in Nodes.Values)
            {
                node.Reset();
            }
        }
    }
}
