using System.Collections.Generic;
using System.Linq;

namespace ShortestPath.models
{
    public class PriorityQueue
    {
        private readonly SortedDictionary<double, HashSet<Node>> dictionary = new SortedDictionary<double, HashSet<Node>>();
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
        public bool IsEmpty()
        {
            return dictionary.Count == 0;
        }
    }
}