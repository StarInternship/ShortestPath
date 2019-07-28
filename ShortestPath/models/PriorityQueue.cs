using System.Collections.Generic;
using System.Linq;

namespace ShortestPath.models
{
    /// <summary>
    /// a sorted queue used in find shortest paths
    /// </summary>
    public class PriorityQueue
    {
        /// <summary>
        /// sorted dictionary of node distance to nodes set havind that distance to source
        /// </summary>
        private readonly SortedDictionary<double, HashSet<Node>> dictionary = new SortedDictionary<double, HashSet<Node>>();

        /// <summary>
        /// adds a node to queue
        /// </summary>
        /// <param name="node">node</param>
        public void Add(Node node)
        {
            if (dictionary.ContainsKey(node.Distance))
            {
                dictionary[node.Distance].Add(node);
            }
            else
            {
                dictionary[node.Distance] = new HashSet<Node> { node };
            }
        }

        /// <summary>
        /// pops first node of queue (removes it)
        /// </summary>
        /// <returns>first node of queue</returns>
        public Node Pop()
        {
            var first = dictionary.First();
            Node firstNode = first.Value.First();
            first.Value.Remove(firstNode);

            if (first.Value.Count == 0)
            {
                dictionary.Remove(first.Key);
            }
            return firstNode;
        }

        /// <summary>
        /// returns true if queue is empty
        /// </summary>
        /// <returns>true if queue is empty</returns>
        public bool IsEmpty() => dictionary.Count == 0;
    }
}