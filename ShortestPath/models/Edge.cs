namespace ShortestPath.models
{
    public class Edge
    {
        public Node From { get; }
        public Node To { get; }
        public double Weight { get; }

        public Edge(Node from, Node to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}