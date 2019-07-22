using System.Collections.Generic;
using System.Linq;

namespace ShortestPath.models
{
    public class PriorityQueue
    {
        private readonly SortedDictionary<double, Node> dictionary = new SortedDictionary<double, Node>();
        public void Add(Node node) => dictionary.Add(node.Distance, node);

        public Node Pop()
        {
            var first = dictionary.First();
            dictionary.Remove(first.Key);
            return first.Value;
        }
        public bool IsEmpty()
        {
            return dictionary.Count == 0;
        }
    }
}
